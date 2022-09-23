using Hangfire;
using Hangfire.DependencyInjection;
using Hangfire.Mongo;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using Shared.Job.Configuration.Filters;
using System.Linq;

namespace Shared.Job.Configuration.Extensions
{
    public static class JobConfigurationExtensions
    {
        public static IServiceCollection AddJobs(this IServiceCollection services, IConfiguration configuration) =>
            services
                .AddOptions<Options.JobOptions>()
                .Bind(configuration.GetHangFireConfiguration())
                .PostConfigure<IConfiguration>((opt, rootCfg) =>
                    opt.StorageConnectionString = rootCfg.GetConnectionString("HangFire_Storage")
                )
                .Services
                .AddJobs();

        public static IServiceCollection AddJobs(this IServiceCollection services, Options.JobOptions options) =>
            services
                .ConfigureOptions(options)
                .AddJobs();        

        private static IServiceCollection AddJobs(this IServiceCollection services) =>
            services
                .AddHangfire((provider, hangfire) =>
                {
                    var jobOptions = provider.GetRequiredService<IOptions<Options.JobOptions>>();

                    hangfire
                        .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                        .UseFilter(new PreserveOriginalQueueAttribute())
                        .UseActivator(new DependencyInjectionJobActivator(provider))
                        .UseMongoStorage(
                            jobOptions.Value.StorageConnectionString,
                            new MongoStorageOptions
                            {
                                CheckQueuedJobsStrategy = CheckQueuedJobsStrategy.Poll,
                                MigrationOptions = new MongoMigrationOptions
                                {
                                    MigrationStrategy = new Hangfire.Mongo.Migration.Strategies.MigrateMongoMigrationStrategy(),
                                    BackupStrategy = new Hangfire.Mongo.Migration.Strategies.Backup.CollectionMongoBackupStrategy()
                                },
                                Prefix = jobOptions.Value.StoragePrefix,
                                CheckConnection = true,
                                ConnectionCheckTimeout = TimeSpan.FromSeconds(30)
                            }
                        );
                });

        public static IServiceCollection AddJobsServer(this IServiceCollection services, IConfiguration configuration) =>
            services
                .AddOptions<Options.JobServerOptions>()
                .Bind(configuration.GetHangFireConfiguration())
                .Services
                .AddJobsServer();

        public static IServiceCollection AddJobsServer(this IServiceCollection services, Options.JobServerOptions options) =>
            services
                .ConfigureOptions(options)
                .AddJobsServer();

        private static IServiceCollection AddJobsServer(this IServiceCollection services) =>
            services
                .AddHangfireServer((provider, options) =>
                {
                    var jobServerOptions = provider.GetRequiredService<IOptions<Options.JobServerOptions>>();

                    options.Activator = new DependencyInjectionJobActivator(provider);
                    options.WorkerCount = jobServerOptions.Value.WorkerCount;

                    if (!string.IsNullOrWhiteSpace(jobServerOptions.Value.QueueName))
                        Array.Fill(options.Queues, jobServerOptions.Value.QueueName);
                });

        private static IConfiguration GetHangFireConfiguration(this IConfiguration configuration) =>
            configuration.GetSection("HangFire") is IConfigurationSection section && section.Exists()
                ? section
                : configuration;
    }
}
