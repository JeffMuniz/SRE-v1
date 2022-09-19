using System;

namespace Integration.Api.Authentication
{
    public class BasicAuthenticationCredential
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string[] Roles { get; set; } = Array.Empty<string>();
    }
}
