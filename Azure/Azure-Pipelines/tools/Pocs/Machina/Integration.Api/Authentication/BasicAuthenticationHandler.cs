using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Integration.Api.Authentication
{
    public class BasicAuthenticationHandler : AuthenticationHandler<BasicAuthenticationOptions>
    {
        public BasicAuthenticationHandler(
            IOptionsMonitor<BasicAuthenticationOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock
        ) : base(options, logger, encoder, clock)
        {
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var endpoint = Context.GetEndpoint();
            if (endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() != null)
                return AuthenticateResult.NoResult();

            if (!Request.Headers.ContainsKey("Authorization"))
                return AuthenticateResult.Fail("Missing Authorization Header");

            try
            {
                var authHeader = AuthenticationHeaderValue.Parse(Context.Request.Headers["Authorization"]);
                if (authHeader.Scheme?.ToLower() != Scheme.Name.ToLower())
                    return AuthenticateResult.Fail("Invalid Authorization Scheme");

                var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
                var credentials = Encoding.UTF8.GetString(credentialBytes).Split(new[] { ':' }, 2);
                var username = credentials[0];
                var password = credentials[1];

                if (Options.Credentials
                        .FirstOrDefault(credential =>
                            credential.Username == username &&
                            credential.Password == password
                        ) is not BasicAuthenticationCredential credential)
                    return AuthenticateResult.Fail("Invalid Username or Password");

                var claims = Enumerable.Concat(
                    new[] {
                        new Claim(ClaimTypes.NameIdentifier, credential.Username),
                        new Claim(ClaimTypes.Name, credential.Username)
                    },
                    credential.Roles.Select(role => new Claim(ClaimTypes.Role, role))
                ).ToArray();
                var identity = new ClaimsIdentity(claims, Scheme.Name);
                var principal = new ClaimsPrincipal(identity);
                var ticket = new AuthenticationTicket(principal, Scheme.Name);

                return await Task.FromResult(
                    AuthenticateResult.Success(ticket)
                );
            }
            catch (Exception ex)
            {
                var errorMessage = "Invalid Authorization Header";

                Logger.LogError(ex, errorMessage);

                return AuthenticateResult.Fail(errorMessage);
            }
        }
    }
}
