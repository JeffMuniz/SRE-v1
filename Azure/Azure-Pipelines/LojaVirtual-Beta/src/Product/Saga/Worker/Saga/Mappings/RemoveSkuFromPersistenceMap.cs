using AutoMapper;
using SagaMessages = Shared.Messaging.Contracts.Product.Saga.Messages;
using SharedMessagingModels = Shared.Messaging.Contracts.Shared.Models;

namespace Product.Saga.Worker.Saga.Mappings
{
    public class RemoveSkuFromPersistenceMap : Profile
    {
        public RemoveSkuFromPersistenceMap()
        {
            CreateMap<States.SkuState, SagaMessages.Persistence.RemoveSku>()
                .IncludeMembers(
                    source => source.SupplierSku
                );

            CreateMap<SharedMessagingModels.SupplierSku, SagaMessages.Persistence.RemoveSku>()
                .ForMember(
                    dest => dest.SupplierId,
                    opt => opt.MapFrom(source => source.SupplierId)
                )
                .ForMember(
                    dest => dest.SupplierSkuId,
                    opt => opt.MapFrom(source => source.SkuId)
                );
        }
    }
}
