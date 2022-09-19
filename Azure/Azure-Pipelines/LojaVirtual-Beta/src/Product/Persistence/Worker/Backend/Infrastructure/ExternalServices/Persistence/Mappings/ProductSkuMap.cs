using AutoMapper;

namespace Product.Persistence.Worker.Backend.Infrastructure.ExternalServices.Persistence.Mappings
{
    public class ProductSkuMap : Profile
    {
        public ProductSkuMap()
        {
            CreateMap<Domain.Entities.ProductSku, Models.Request.ProductSku>()
                .ForMember(
                    dest => dest.ProductSkuId,
                    opt => opt.MapFrom(source => source.Id)
                )
                .ForMember(
                    dest => dest.SkuStatusId,
                    opt => opt.MapFrom((source, dest) => source.SkuStatus.ToInteger())
                )
                .ForMember(
                    dest => dest.SkuFeatures,
                    opt => opt.MapFrom(source => source.SkuFeatures)
                )
                .ReverseMap();

            CreateMap<Domain.ValueObjects.EnrichedSku, Models.Request.EnrichedSku>()
                .ReverseMap();
        }
    }
}
