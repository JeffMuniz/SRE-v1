using AutoMapper;

namespace Product.Enrichment.macnaima.Api.Backend.Application.Usecases.GetOfferDetails.Mappings
{
    public class OutboundMap : Profile
    {
        public OutboundMap()
        {
            CreateMap<Models.Outbound, Domain.Entities.Offer>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(source => source.OfferId)
                )
                .ForMember(
                    dest => dest.SupplierId,
                    opt => opt.MapFrom(source => source.SellerId)
                )
                .ReverseMap();
        }
    }
}
