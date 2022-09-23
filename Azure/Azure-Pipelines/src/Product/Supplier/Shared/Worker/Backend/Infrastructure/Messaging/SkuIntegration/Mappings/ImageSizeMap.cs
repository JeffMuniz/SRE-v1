using AutoMapper;
using MessagingContracts = Shared.Messaging.Contracts;
using SharedDomain = Shared.Backend.Domain;

namespace Product.Supplier.Shared.Worker.Backend.Infrastructure.Messaging.SkuIntegration.Mappings
{
    public class ImageSizeMap : Profile
    {
        public ImageSizeMap()
        {
            CreateMap<SharedDomain.ValueObjects.ImageSize, MessagingContracts.Shared.Models.ImageSize>()
                .ForMember(
                    dest => dest.Size,
                    opt => opt.MapFrom(source => source.Size.Id)
                )
                .ReverseMap();
        }
    }
}
