using AutoMapper;
using SharedMessagingContracts = Shared.Messaging.Contracts.Shared;
using SharedUsecases = Shared.Backend.Application.Usecases;

namespace Shared.Backend.Consumers.Messaging.Mappings
{
    public class ImageSizeMap : Profile
    {
        public ImageSizeMap()
        {
            CreateMap<SharedUsecases.Models.ImageSize, SharedMessagingContracts.Models.ImageSize>()
                .ReverseMap();
        }
    }
}
