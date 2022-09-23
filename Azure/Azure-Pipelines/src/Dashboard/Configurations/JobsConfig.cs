using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Dashboard.Web.Configurations
{
    internal static class JobsConfig
    {
        public static IApplicationBuilder UseJobsDashboard(this IApplicationBuilder app)
        {
            return app
                .UseEndpoints(endpoints =>
                {
                    var configuration = app.ApplicationServices.GetRequiredService<IConfiguration>();
                    var dashboardPath = configuration.GetValue<string>("HangFire:DashboardPath");

                    endpoints
                        .MapHangfireDashboardWithAuthorizationPolicy(
                            "",
                            dashboardPath,
                            new DashboardOptions
                            {
                                AppPath = dashboardPath,
                                DashboardTitle = configuration.GetValue<string>("HangFire:DashboardTitle")
                            }
                        )
                        .AllowAnonymous();
                });
        }
    }
}
