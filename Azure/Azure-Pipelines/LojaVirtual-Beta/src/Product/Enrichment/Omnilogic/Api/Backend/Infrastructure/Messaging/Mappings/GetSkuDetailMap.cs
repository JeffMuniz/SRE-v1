using AutoMapper;
using Change = Shared.Messaging.Contracts.Product.Change.Messages;

namespace Product.Enrichment.macnaima.Api.Backend.Infrastructure.Messaging.Mappings
{
    public class GetSkuDetailMap : Profile
    {
        public GetSkuDetailMap()
        {
            CreateMap<Domain.ValueObjects.OfferId, Change.GetSkuDetail>()
                .ForMember(
                    dest => dest.SkuIntegrationId,
                    opt => opt.MapFrom(source => source.Value)
                )
                .ReverseMap();

            CreateMap<Domain.Entities.Offer, Change.GetSkuDetailResponse>()
                .ForMember(
                    dest => dest.SkuIntegrationId,
                    opt => opt.MapFrom(source => source.Id)
                )
                .ForMember(
                    dest => dest.SupplierSku,
                    opt => opt.MapFrom(source => source)
                )
                .ReverseMap();
        }
    }
}
