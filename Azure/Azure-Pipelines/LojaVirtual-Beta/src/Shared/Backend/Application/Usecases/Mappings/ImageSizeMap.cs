using AutoMapper;
using SharedDomain = Shared.Backend.Domain;

namespace Shared.Backend.Application.Usecases.Mappings
{
    public class ImageSizeMap : Profile
    {
        public ImageSizeMap()
        {
            CreateMap<Models.ImageSize, SharedDomain.ValueObjects.ImageSize>()
                .ForMember(
                    dest => dest.Size,
                    opt => opt.MapFrom((source, dest) => SharedDomain.ValueObjects.ImageSizeType.FromId(source.Size).GetValueOrDefault())
                )
                .ReverseMap()
                .ForMember(
                    dest => dest.Size,
                    opt => opt.MapFrom(source => source.Size.Id)
                );
        }
    }
}
