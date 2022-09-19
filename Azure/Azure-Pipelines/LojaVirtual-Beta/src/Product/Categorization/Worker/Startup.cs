using GreenPipes;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Product.Categorization.Worker.Backend.Application.Usecases.CategorizeSku;
using Product.Categorization.Worker.Backend.Application.Usecases.CategorizeSku.Options;
using Product.Categorization.Worker.Backend.Domain.Repositories;
using Product.Categorization.Worker.Backend.Domain.Services;
using Product.Categorization.Worker.Backend.Infrastructure.Categorizer;
using Product.Categorization.Worker.Backend.Infrastructure.Persistence.KnowledgeData.Context;
using Product.Categorization.Worker.Backend.Infrastructure.Persistence.KnowledgeData.Repositories;
using Product.Categorization.Worker.Consumers.CategorizeSku;
using Shared.Messaging.Configuration;
using Shared.Messaging.Configuration.Extensions;
using Shared.Messaging.Contracts.Product.Saga.Messages.Categorization;
using Shared.Persistence.Mongo.Extensions;
using System;
using BaseWorker = Shared.Worker;

namespace Product.Categorization.Worker
{
    public class Startup : BaseWorker.Startup
    {
        public Startup(IConfiguration configuration)
            : base(configuration)
        {
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            services
                .AddMemoryCache();

            services
                 .AddMessaging(
                     _configuration.GetConnectionString("ServiceBus"),
                     masstransit =>
                     {
                         masstransit.AddConsumer<CategorizeSkuConsumer>();
                     },
                     (provider, config) =>
                     {
                         config.UseMessageRetry(retry =>
                            retry.Interval(2, TimeSpan.FromSeconds(5))
                         );

                         EndpointConfigurator.MapCommand<CategorizeSku>();
                         EndpointConfigurator.MapEvent<SkuCategorized>();

                         config.ConfigureReceive<CategorizeSku, CategorizeSkuConsumer>(provider,
                             endpoint =>
                             {
                                 var concurrentMessageLimit = _configuration.GetValue("Consumers:CategorizeSku:ConcurrentMessageLimit", 1);
                                 endpoint.PrefetchCount = concurrentMessageLimit * 2;
                                 endpoint.ConcurrentMessageLimit = concurrentMessageLimit;
                             });
                     }
                 );

            services
                .AddMongoContext<CategorizationContext>(
                    _configuration.GetSection("Mongo:Categorization"),
                    options =>
                    {
                        options.ApplicationName = _configuration.GetValue<string>("Mongo:ApplicationName");
                        options.ConnectionString = _configuration.GetConnectionString("Categorization");
                    }
                 )
                .AddSingleton<ICategorizationContext, CategorizationContext>();

            services
                .Configure<CategorizeOptions>(_configuration.GetSection("Categorize"));

            services
                .AddSingleton<ITratamentoDados, TratamentoDados>()
                .AddScoped<ICategorizerService, CategorizerService>()
                .AddScoped<IKnowledgeDataRepository, KnowledgeDataRepository>()
                .AddScoped<ICategoryRepository, CategoryRepository>()
                .AddScoped<ICategorizeSkuUsecase, CategorizeSkuUsecase>();
        }
    }
}
