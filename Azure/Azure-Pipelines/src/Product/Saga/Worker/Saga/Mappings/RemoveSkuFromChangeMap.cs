using AutoMapper;
using SagaMessages = Shared.Messaging.Contracts.Product.Saga.Messages;
using SharedMessagingModels = Shared.Messaging.Contracts.Shared.Models;

namespace Product.Saga.Worker.Saga.Mappings
{
    public class RemoveSkuFromChangeMap : Profile
    {
        public RemoveSkuFromChangeMap()
        {
            CreateMap<SagaMessages.Change.RemoveSku, States.SkuState>()
                .ForMember(
                    dest => dest.SkuIntegrationId,
                    opt => opt.MapFrom(source => source.SkuIntegrationId)
                )
                .ForMember(
                    dest => dest.SupplierSku,
                    opt => opt.MapFrom(source => source)
                )
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<SagaMessages.Change.RemoveSku, SharedMessagingModels.SupplierSku>()
                .ForMember(
                    dest => dest.SkuId,
                    opt => opt.MapFrom(source => source.SupplierSkuId)
                )
                .ReverseMap();
        }
    }
}
