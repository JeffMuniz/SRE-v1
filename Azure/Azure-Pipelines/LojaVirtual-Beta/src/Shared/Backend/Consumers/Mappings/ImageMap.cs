using AutoMapper;
using SharedMessagingContracts = Shared.Messaging.Contracts.Shared;
using SharedUsecases = Shared.Backend.Application.Usecases;

namespace Shared.Backend.Consumers.Messaging.Mappings
{
    public class ImageMap : Profile
    {
        public ImageMap()
        {
            CreateMap<SharedUsecases.Models.Image, SharedMessagingContracts.Models.Image>()
                .ReverseMap();
        }
    }
}
