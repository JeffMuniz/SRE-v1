using AutoMapper;
using SharedDomain = Shared.Backend.Domain;
using SharedMessagingContracts = Shared.Messaging.Contracts.Shared;

namespace Shared.Backend.Infrastructure.Messaging.Mappings
{
    public class PriceMap : Profile
    {
        public PriceMap()
        {
            CreateMap<SharedDomain.ValueObjects.Price, SharedMessagingContracts.Models.Price>()
                .ReverseMap();
        }
    }
}
