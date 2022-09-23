using Integration.Api.Authentication;
using Integration.Api.Backend.Application.Offer;
using Integration.Api.Backend.Application.Offer.UseCases.Create;
using Integration.Api.Backend.Application.Offer.UseCases.GetDetail;
using Integration.Api.Backend.Application.Offer.UseCases.ImportCatalog;
using Integration.Api.Backend.Application.Offer.UseCases.MakeEnrich;
using Integration.Api.Backend.Application.Offer.UseCases.NotifyPendings;
using Integration.Api.Backend.Domain.ExternalServices;
using Integration.Api.Backend.Domain.Repositories;
using Integration.Api.Backend.Infrastructure.ExternalServices;
using Integration.Api.Backend.Infrastructure.Persistence.Databases.Catalog;
using Integration.Api.Backend.Infrastructure.Persistence.Databases.Integration;
using Integration.Api.Backend.Infrastructure.Persistence.Repositories;
using Integration.Api.BackgroundServices;
using Integration.Api.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System;
using System.Net.Http.Headers;

namespace Integration.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddCors(options =>
                {
                    options.AddPolicy("CorsPolicy",
                        builder => builder
                            .AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowCredentials()
                    );
                })
                .AddAuthentication(BasicAuthenticationDefaults.AuthenticationScheme)
                .AddBasicAuthentication(options =>
                {
                    options.ClaimsIssuer = Configuration.GetValue<string>("Authentication:Basic:Issuer");
                    options.Credentials = Configuration.GetSection("Authentication:Basic:Credentials")
                        .Get<BasicAuthenticationCredential[]>();
                });

            services
                .AddControllers()
                .AddNewtonsoftJson(cfg => cfg.UseMemberCasing());

            services
                .AddSwaggerGenNewtonsoftSupport()
                .AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Catalog Enrichment Integration Api", Version = "v1" });

                    var basicSecurityScheme = new OpenApiSecurityScheme
                    {
                        Scheme = BasicAuthenticationDefaults.AuthenticationScheme,
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.Http,
                        Description = "Please enter into field the token",
                        Reference = new OpenApiReference
                        {
                            Id = BasicAuthenticationDefaults.AuthenticationScheme,
                            Type = ReferenceType.SecurityScheme
                        }
                    };
                    c.AddSecurityDefinition(
                        basicSecurityScheme.Reference.Id,
                        basicSecurityScheme
                    );
                    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        { basicSecurityScheme, Array.Empty<string>() }
                    });
                });

            services
                .AddAutoMapper(System.Reflection.Assembly.GetExecutingAssembly());

            services
                .Configure<IntegrationDatabaseSettings>(options =>
                {
                    options.ConnectionString = Configuration.GetConnectionString("Integration");
                    options.DatabaseName = Configuration.GetConnectionString("Integration.DatabaseName");
                })
                .Configure<CatalogDatabaseSettings>(options =>
                {
                    options.ConnectionString = Configuration.GetConnectionString("Catalog");
                });

            services
                .AddOptions<MacnaimaServiceSettings>()
                .BindConfiguration("Macnaima");

            services
                .AddOptions<CatalogOfferImportBackgroundServiceOptions>()
                .BindConfiguration("BackgroundServices:CatalogOfferImport");

            services
                .AddOptions<OfferNotificationBackgroundServiceOptions>()
                .BindConfiguration("BackgroundServices:OfferNotification");

            services
                .AddHttpClient<IMacnaimaService, MacnaimaService>((provider, client) =>
                {
                    var settings = provider.GetRequiredService<IOptionsMonitor<MacnaimaServiceSettings>>();
                    client.BaseAddress = settings.CurrentValue.BaseAddress;
                    client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(settings.CurrentValue.AccessKey);
                });

            services
                .AddHostedService<CatalogOfferImportBackgroundService>()
                .AddHostedService<OfferNotificationBackgroundService>()

                .AddScoped<ICreateUseCase, CreateUseCase>()
                .AddScoped<IGetDetailUseCase, GetDetailUseCase>()
                .AddScoped<IImportCatalogUseCase, ImportCatalogUseCase>()
                .AddScoped<IMakeEnrichUseCase, MakeEnrichUseCase>()
                .AddScoped<INotifyPendingsUseCase, NotifyPendingsUseCase>()

                .AddSingleton<IIntegrationDatabase, IntegrationDatabase>()
                .AddScoped<ICatalogDatabase, CatalogDatabase>()

                .AddScoped<IOfferNotificationRepository, OfferNotificationRepository>()
                .AddScoped<IOfferNotificationHistoryRepository, OfferNotificationHistoryRepository>()
                .AddScoped<ICatalogImportSettingsRepository, CatalogImportSettingsRepository>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (!env.IsProduction())
            {
                app.UseDeveloperExceptionPage();
            }

            if (Configuration.GetValue("LoggingRequestBody", false))
                app.UseTraceRequestLogging();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                c.DisplayRequestDuration();
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
