using Microsoft.AspNetCore.Authentication;
using System;

namespace Integration.Api.Authentication
{
    public static class BasicAuthenticationExtensions
    {
        public static AuthenticationBuilder AddBasicAuthentication(this AuthenticationBuilder builder, Action<BasicAuthenticationOptions> configureOptions = null) =>
            builder.AddScheme<BasicAuthenticationOptions, BasicAuthenticationHandler>(BasicAuthenticationDefaults.AuthenticationScheme, configureOptions);
    }
}
