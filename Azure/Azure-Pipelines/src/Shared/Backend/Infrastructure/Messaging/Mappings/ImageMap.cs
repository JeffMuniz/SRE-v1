using AutoMapper;
using SharedDomain = Shared.Backend.Domain;
using SharedMessagingContracts = Shared.Messaging.Contracts.Shared;

namespace Shared.Backend.Infrastructure.Messaging.Mappings
{
    public class ImageMap : Profile
    {
        public ImageMap()
        {
            CreateMap<SharedDomain.ValueObjects.Image, SharedMessagingContracts.Models.Image>()
                .ReverseMap();
        }
    }
}
