using AutoMapper;
using SharedDomain = Shared.Backend.Domain;
using SharedMessagingContracts = Shared.Messaging.Contracts.Shared;

namespace Shared.Backend.Infrastructure.Messaging.Mappings
{
    public class ImageSizeMap : Profile
    {
        public ImageSizeMap()
        {
            CreateMap<SharedDomain.ValueObjects.ImageSize, SharedMessagingContracts.Models.ImageSize>()
                .ForMember(
                    dest => dest.Size,
                    opt => opt.MapFrom(source => source.Size.Id)
                )                
                .ReverseMap()
                .ForMember(
                    dest => dest.Size,
                    opt => opt.MapFrom((source, dest) => SharedDomain.ValueObjects.ImageSizeType.FromId(source.Size).GetValueOrDefault())
                );
        }
    }
}
