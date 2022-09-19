using AutoMapper;
using Saga = Shared.Messaging.Contracts.Product.Saga.Messages;

namespace Product.Enrichment.macnaima.Api.Backend.Infrastructure.Messaging.Mappings
{
    public class UpdateSkuEnrichedMap : Profile
    {
        public UpdateSkuEnrichedMap()
        {
            CreateMap<Domain.Entities.EnrichedOffer, Saga.Enrichment.UpdateSkuEnriched>()
                .ForMember(
                    dest => dest.SkuIntegrationId,
                    opt => opt.MapFrom(source => source.Id)
                )
                .ForMember(
                    dest => dest.SupplierSkuId,
                    opt => opt.MapFrom(source => source.SkuId)
                )
                .ReverseMap();

            CreateMap<Domain.Entities.EnrichedOffer, Saga.Enrichment.UpdateSkuEnriched>()
                .ForMember(
                    dest => dest.SkuIntegrationId,
                    opt => opt.MapFrom(source => source.Id)
                )
                .ForMember(
                    dest => dest.SupplierSkuId,
                    opt => opt.MapFrom(source => source.SkuId)
                )
                .ReverseMap();
        }
    }
}
