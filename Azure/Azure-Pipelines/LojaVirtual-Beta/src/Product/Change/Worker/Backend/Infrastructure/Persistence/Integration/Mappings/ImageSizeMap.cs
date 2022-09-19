using AutoMapper;
using SharedDomain = Shared.Backend.Domain;

namespace Product.Change.Worker.Backend.Infrastructure.Persistence.Integration.Mappings
{
    public class ImageSizeMap : Profile
    {
        public ImageSizeMap()
        {
            CreateMap<SharedDomain.ValueObjects.ImageSize, Entities.ImageSize>()
                .ForMember(
                    dest => dest.Size,
                    opt => opt.MapFrom((source, dest) => source.Size.Id)
                )
                .ReverseMap()
                .ForMember(
                    dest => dest.Size,
                    opt => opt.MapFrom((source, dest) => SharedDomain.ValueObjects.ImageSizeType.FromId(source.Size).GetValueOrDefault())
                );
        }
    }
}
