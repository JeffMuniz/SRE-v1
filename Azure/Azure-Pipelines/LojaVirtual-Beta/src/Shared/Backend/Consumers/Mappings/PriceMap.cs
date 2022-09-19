using AutoMapper;
using SharedMessagingContracts = Shared.Messaging.Contracts.Shared;
using SharedUsecases = Shared.Backend.Application.Usecases;

namespace Shared.Backend.Consumers.Messaging.Mappings
{
    public class PriceMap : Profile
    {
        public PriceMap()
        {
            CreateMap<SharedUsecases.Models.Price, SharedMessagingContracts.Models.Price>()
                .ReverseMap();
        }
    }
}
