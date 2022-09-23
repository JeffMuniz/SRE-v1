using AutoMapper;
using Integration.Api.Backend.Application.Offer.Models;

namespace Integration.Api.Backend.Application.Offer.Mappings
{
    public class OfferMap : Profile
    {
        public OfferMap()
        {
            CreateMap<Domain.Entities.OfferNotification, OfferModel>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(source => source.Id)
                )
                .IncludeMembers(source => source.Offer)
                .ReverseMap();

            CreateMap<Domain.Entities.Offer, OfferModel>()
                .IncludeMembers(source => source.Id)
                .ReverseMap();

            CreateMap<Domain.ValueObjects.OfferId, OfferModel>()
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
