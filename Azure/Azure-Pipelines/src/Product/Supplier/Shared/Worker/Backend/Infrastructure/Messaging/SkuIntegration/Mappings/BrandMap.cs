using AutoMapper;
using MessagingContracts = Shared.Messaging.Contracts;

namespace Product.Supplier.Shared.Worker.Backend.Infrastructure.Messaging.SkuIntegration.Mappings
{
    public class BrandMap : Profile
    {
        public BrandMap()
        {
            CreateMap<Domain.Entities.Brand, MessagingContracts.Shared.Models.Brand>()
                .ReverseMap();
        }
    }
}
