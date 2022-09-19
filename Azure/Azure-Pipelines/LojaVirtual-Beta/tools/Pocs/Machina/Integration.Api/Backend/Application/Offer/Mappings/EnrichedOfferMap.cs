using AutoMapper;
using Integration.Api.Backend.Application.Offer.Models;
using Integration.Api.Backend.Domain.ValueObjects;

namespace Integration.Api.Backend.Application.Offer.Mappings
{
    public class EnrichedOfferMap : Profile
    {
        public EnrichedOfferMap()
        {
            CreateMap<EnrichedOffer, EnrichedOfferModel>()
                .ReverseMap();
        }
    }
}
