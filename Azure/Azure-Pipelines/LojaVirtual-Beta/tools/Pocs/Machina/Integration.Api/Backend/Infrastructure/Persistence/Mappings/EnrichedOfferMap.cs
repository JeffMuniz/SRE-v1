using AutoMapper;
using Integration.Api.Backend.Infrastructure.Persistence.Entities;

namespace Integration.Api.Backend.Infrastructure.Persistence.Mappings
{
    public class EnrichedOfferMap : Profile
    {
        public EnrichedOfferMap()
        {
            CreateMap<Domain.ValueObjects.EnrichedOffer, EnrichedOffer>()
                .ReverseMap();
        }
    }
}
