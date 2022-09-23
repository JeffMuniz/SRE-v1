using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product.Enrichment.Macnaima.Api.Infrastructure.Web.Authentication
{
    public class BasicAuthenticationCredential
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string[] Roles { get; set; } = Array.Empty<string>();
    }
}
