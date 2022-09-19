using AutoMapper;
using MessagingContracts = Shared.Messaging.Contracts;
using SharedDomain = Shared.Backend.Domain;

namespace Product.Supplier.Shared.Worker.Backend.Infrastructure.Messaging.SkuIntegration.Mappings
{
    public class PriceMap : Profile
    {
        public PriceMap()
        {
            CreateMap<SharedDomain.ValueObjects.Price, MessagingContracts.Shared.Models.Price>()
                .ReverseMap();
        }
    }
}
