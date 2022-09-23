using AutoMapper;
using MessagingContracts = Shared.Messaging.Contracts;

namespace Product.Enrichment.Macnaima.Worker.Backend.Application.Shared.Mappings
{
    public class SendSkuForEnrichmentMap : Profile
    {
        public SendSkuForEnrichmentMap()
        {
            CreateMap<MessagingContracts.Product.Saga.Messages.Enrichment.SendSkuForEnrichment, Usecases.NotifyPendingOffer.Models.Inbound>()
                .ForMember(
                    dest => dest.OfferId,
                    opt => opt.MapFrom(source => source.SkuIntegrationId)
                )
                .ReverseMap();
        }
    }
}
