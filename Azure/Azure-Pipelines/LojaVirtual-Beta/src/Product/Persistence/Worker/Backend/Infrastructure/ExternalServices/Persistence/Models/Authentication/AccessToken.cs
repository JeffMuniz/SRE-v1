using System;

namespace Product.Persistence.Worker.Backend.Infrastructure.ExternalServices.Persistence.Models.Authentication
{
    public class AccessToken
    {
        public string Token { get; set; }

        public DateTime ExpirationDate { get; set; }
    }
}
