using AutoMapper;
using Product.Enrichment.macnaima.Api.Endpoints.Models;
using Usecases = Product.Enrichment.macnaima.Api.Backend.Application.Usecases;

namespace Product.Enrichment.macnaima.Api.Endpoints.Mappings
{
    public class EnrichedOfferMap : Profile
    {
        public EnrichedOfferMap()
        {
            CreateMap<OfferIdModel, Usecases.MakeEnrich.Models.Inbound>()
                .ReverseMap();

            CreateMap<EnrichedOfferModel, Usecases.MakeEnrich.Models.Inbound>()
                .ForMember(
                    dest => dest.SkuId,
                    opt => opt.MapFrom(source => source.Sku)
                )
                .ReverseMap();
        }
    }
}
