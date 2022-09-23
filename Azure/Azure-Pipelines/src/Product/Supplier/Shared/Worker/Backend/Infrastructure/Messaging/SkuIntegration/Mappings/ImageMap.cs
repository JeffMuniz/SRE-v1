using AutoMapper;
using MessagingContracts = Shared.Messaging.Contracts;
using SharedDomain = Shared.Backend.Domain;

namespace Product.Supplier.Shared.Worker.Backend.Infrastructure.Messaging.SkuIntegration.Mappings
{
    public class ImageMap : Profile
    {
        public ImageMap()
        {
            CreateMap<SharedDomain.ValueObjects.Image, MessagingContracts.Shared.Models.Image>()
                .ReverseMap();
        }
    }
}
