using HealthChecks.UI.Client;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Product.Enrichment.Macnaima.Api.Backend.Application.Usecases.GetOfferDetails;
using Product.Enrichment.Macnaima.Api.Backend.Application.Usecases.MakeEnrich;
using Product.Enrichment.Macnaima.Api.Backend.Domain.Services;
using Product.Enrichment.Macnaima.Api.Backend.Infrastructure.Messaging;
using Product.Enrichment.Macnaima.Api.Configurations;
using Product.Enrichment.Macnaima.Api.Infrastructure.Web.Authentication;
using Product.Enrichment.Macnaima.Api.Infrastructure.Web.Filters;
using Shared.Messaging.Configuration;
using Shared.Messaging.Configuration.Extensions;
using System;
using ChangeMessages = Shared.Messaging.Contracts.Product.Change.Messages;
using SagaMessages = Shared.Messaging.Contracts.Product.Saga.Messages;

namespace Product.Enrichment.Macnaima.Api
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
                .AddMessaging(
                    Configuration.GetConnectionString("ServiceBus"),
                    masstransit =>
                    {
                        masstransit.AddRequestClient<ChangeMessages.GetSkuDetail>(TimeSpan.FromMinutes(1));
                    },
                    (provider, config) =>
                    {
                        EndpointConfigurator.MapCommand<ChangeMessages.GetSkuDetail>();
                        EndpointConfigurator.MapCommand<SagaMessages.Enrichment.UpdateSkuEnriched>();
                    }
                );

            services
                .AddHealthChecks()
                .AddAzureServiceBusQueue(
                    Configuration.GetConnectionString("ServiceBus"),
                    EndpointConfigurator.GetEndpointName<ChangeMessages.GetSkuDetail>(),
                    "OfferDetails"
                )
                .AddAzureServiceBusQueue(
                    Configuration.GetConnectionString("ServiceBus"),
                    EndpointConfigurator.GetEndpointName<SagaMessages.Enrichment.UpdateSkuEnriched>(),
                    "EnrichedOffer"
                );

            services
                .AddSwaggerGenNewtonsoftSupport()
                .AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Catalog Enrichment Integration Api", Version = "v1" });

                    c.OperationFilter<CommonResponsesOperationFilter>();

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
                .AddCustomMappers()
                .AddControllers(options =>
                    options.Filters.Add(typeof(HttpResponseExceptionFilter))
                )
                .AddNewtonsoftJson(cfg => cfg.UseMemberCasing());

            services
                .AddScoped<IOfferDetailsService, OfferDetailsService>()
                .AddScoped<IGetOfferDetailsUsecases, GetOfferDetailsUsecases>()
                .AddScoped<IEnrichedOfferService, EnrichedOfferService>()
                .AddScoped<IMakeEnrichUsecase, MakeEnrichUsecase>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app
                    .UseDeveloperExceptionPage();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                c.DisplayRequestDuration();
            });

            app
                //.UseHttpsRedirection()
                .UseRouting()
                .UseHealthChecks("/health")
                .UseHealthChecks("/health/details",
                    new HealthCheckOptions
                    {
                        Predicate = _ => true,
                        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                    }
                );

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
