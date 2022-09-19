using AutoMapper;
using SharedMessagingContracts = Shared.Messaging.Contracts.Shared;
using SharedUsecases = Shared.Backend.Application.Usecases;

namespace Shared.Backend.Consumers.Messaging.Mappings
{
    public class BrandMap : Profile
    {
        public BrandMap()
        {
            CreateMap<SharedUsecases.Models.Brand, SharedMessagingContracts.Models.Brand>()
                .ReverseMap();
        }
    }
}
