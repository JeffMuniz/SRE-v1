using AutoMapper;
using Product.Enrichment.macnaima.Api.Endpoints.Models;
using Usecases = Product.Enrichment.macnaima.Api.Backend.Application.Usecases;

namespace Product.Enrichment.macnaima.Api.Endpoints.Mappings
{
    public class GetOfferDetailsMap : Profile
    {
        public GetOfferDetailsMap()
        {
            CreateMap<OfferIdModel, Usecases.GetOfferDetails.Models.Inbound>()
                .ReverseMap();

            CreateMap<Usecases.GetOfferDetails.Models.Outbound, OfferModel>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(source => source.OfferId)
                )
                .ForMember(
                    dest => dest.Sku,
                    opt => opt.MapFrom(source => source.SkuId)
                )
                .ForMember(
                    dest => dest.Product,
                    opt => opt.MapFrom(source => source.ProductId)
                )
                .ForMember(
                    dest => dest.SkuTitle,
                    opt => opt.MapFrom(source => source.Name)
                )
                .ForMember(
                    dest => dest.Price,
                    opt => opt.MapFrom(source => source.Price.For)
                )
                .ForMember(
                    dest => dest.ListPrice,
                    opt => opt.MapFrom(source => source.Price.From)
                )
                .ReverseMap();
        }
    }
}
