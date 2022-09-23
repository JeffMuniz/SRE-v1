using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product.Enrichment.Macnaima.Api.Infrastructure.Web.Authentication
{
    public class BasicAuthenticationOptions : AuthenticationSchemeOptions
    {
        public BasicAuthenticationCredential[] Credentials { get; set; }
    }
}
