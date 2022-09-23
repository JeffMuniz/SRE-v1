using AutoMapper;
using SagaMessages = Shared.Messaging.Contracts.Product.Saga.Messages;
using SharedMessagingModels = Shared.Messaging.Contracts.Shared.Models;

namespace Product.Saga.Worker.Saga.Mappings
{
    public class UpdateSkuMap : Profile
    {
        public UpdateSkuMap()
        {
            CreateMap<SagaMessages.Change.UpdateSku, States.SkuState>()
                .ForMember(
                    dest => dest.SkuIntegrationId,
                    opt => opt.MapFrom(source => source.SkuIntegrationId)
                )
                .ForMember(
                    dest => dest.SupplierSku,
                    opt => opt.MapFrom(source => source.SupplierSku)
                )
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<SagaMessages.Change.UpdateSku, SharedMessagingModels.SupplierSku>()
                .IncludeMembers(source => source.SupplierSku)
                .ReverseMap();
        }
    }
}

