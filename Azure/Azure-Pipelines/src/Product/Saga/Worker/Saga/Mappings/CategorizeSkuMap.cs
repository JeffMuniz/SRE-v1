using AutoMapper;
using SagaMessages = Shared.Messaging.Contracts.Product.Saga.Messages;

namespace Product.Saga.Worker.Saga.Mappings
{
    public class CategorizeSkuMap : Profile
    {
        public CategorizeSkuMap()
        {

            CreateMap<States.SkuState, SagaMessages.Categorization.CategorizeSku>()
                .ForMember(
                    dest => dest.SkuIntegrationId,
                    opt => opt.MapFrom(source => source.SkuIntegrationId)
                )
                .ForMember(
                    dest => dest.SupplierSku,
                    opt => opt.MapFrom(source => source.SupplierSku)
                );
        }
    }
}
