using AutoMapper;
using SagaMessages = Shared.Messaging.Contracts.Product.Saga.Messages;
using SharedMessagingModels = Shared.Messaging.Contracts.Shared.Models;

namespace Product.Saga.Worker.Saga.Mappings
{
    public class RemoveSkuFromSearchIndexMap : Profile
    {
        public RemoveSkuFromSearchIndexMap()
        {
            CreateMap<States.SkuState, SagaMessages.Search.RemoveSkuFromSearchIndex>()
                .IncludeMembers(
                    source => source.SupplierSku
                );

            CreateMap<SharedMessagingModels.SupplierSku, SagaMessages.Search.RemoveSkuFromSearchIndex>()
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
