using Microsoft.AspNetCore.Authentication;

namespace Integration.Api.Authentication
{
    public class BasicAuthenticationOptions : AuthenticationSchemeOptions
    {
        public BasicAuthenticationCredential[] Credentials { get; set; }
    }
}
