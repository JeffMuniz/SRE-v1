using AutoMapper;
using SharedMessagingContracts = Shared.Messaging.Contracts.Shared;
using SharedUsecases = Shared.Backend.Application.Usecases;

namespace Shared.Backend.Consumers.Messaging.Mappings
{
    public class CategoryMap : Profile
    {
        public CategoryMap()
        {
            CreateMap<SharedUsecases.Models.Category, SharedMessagingContracts.Models.Category>()
                .ReverseMap();
        }
    }
}
