using AutoMapper;
using Integration.Api.Backend.Infrastructure.Persistence.Entities;

namespace Integration.Api.Backend.Infrastructure.Persistence.Mappings
{
    public class OfferMap : Profile
    {
        public OfferMap()
        {
            CreateMap<Domain.Entities.Offer, Offer>()
                .IncludeMembers(source => source.Id)
                .ForMember(
                    dest => dest.Active,
                    opt => opt.MapFrom(source => source.Active)
                )
                .ReverseMap();

            CreateMap<Domain.ValueObjects.OfferId, Offer>()
                .ForMember(
                    dest => dest.SellerId,
                    opt => opt.MapFrom(source => source.SellerId)
                )
                .ForMember(
                    dest => dest.Sku,
                    opt => opt.MapFrom(source => source.Sku)
                )
                .ReverseMap();
        }
    }
}
