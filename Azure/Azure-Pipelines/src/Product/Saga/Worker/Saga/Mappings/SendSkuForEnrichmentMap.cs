using AutoMapper;
using SagaMessages = Shared.Messaging.Contracts.Product.Saga.Messages;
using SharedMessagingModels = Shared.Messaging.Contracts.Shared.Models;

namespace Product.Saga.Worker.Saga.Mappings
{
    public class SendSkuForEnrichmentMap : Profile
    {
        public SendSkuForEnrichmentMap()
        {
            CreateMap<States.SkuState, SagaMessages.Enrichment.SendSkuForEnrichment>()
                .IncludeMembers(
                    source => source.SupplierSku
                );

            CreateMap<SharedMessagingModels.SupplierSku, SagaMessages.Enrichment.SendSkuForEnrichment>()
                .ForMember(
                    dest => dest.SupplierId,
                    opt => opt.MapFrom(source => source.SupplierId)
                )
                .ForMember(
                    dest => dest.SkuId,
                    opt => opt.MapFrom(source => source.SkuId)
                );
        }
    }
}

