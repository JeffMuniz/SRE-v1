using AutoMapper;
using SagaMessages = Shared.Messaging.Contracts.Product.Saga.Messages;
using SharedModels = Shared.Messaging.Contracts.Shared.Models;

namespace Product.Saga.Worker.Saga.Mappings
{
    public class SkuCategorizedMap : Profile
    {
        public SkuCategorizedMap()
        {
            CreateMap<SharedModels.CategorizedData, SagaMessages.Categorization.SkuCategorized>()
                .ForMember(
                    dest => dest.CategorizedData,
                    opt => opt.MapFrom(source => source)
                )
                .ReverseMap();
        }
    }
}
