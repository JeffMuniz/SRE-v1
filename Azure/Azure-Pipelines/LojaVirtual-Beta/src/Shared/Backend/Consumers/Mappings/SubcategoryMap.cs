using AutoMapper;
using SharedMessagingContracts = Shared.Messaging.Contracts.Shared;
using SharedUsecases = Shared.Backend.Application.Usecases;

namespace Shared.Backend.Consumers.Messaging.Mappings
{
    public class SubcategoryMap : Profile
    {
        public SubcategoryMap()
        {
            CreateMap<SharedUsecases.Models.Subcategory, SharedMessagingContracts.Models.Subcategory>()
                .ReverseMap();
        }
    }
}
