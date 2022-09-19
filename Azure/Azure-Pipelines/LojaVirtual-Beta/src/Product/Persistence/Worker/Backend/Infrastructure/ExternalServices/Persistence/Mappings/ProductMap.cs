using AutoMapper;

namespace Product.Persistence.Worker.Backend.Infrastructure.ExternalServices.Persistence.Mappings
{
    public class ProductMap : Profile
    {
        public ProductMap()
        {
            CreateMap<Domain.Entities.Product, Models.Request.Product>()
                .ForMember(
                    dest => dest.SubcategoryId,
                    opt => opt.MapFrom(source => source.SubcategoryId)
                )
                .ForMember(
                    dest => dest.ProductSkus,
                    opt => opt.MapFrom(source => new[] { source.Sku })
                )
                .ForMember(
                    dest => dest.ProductFeatures,
                    opt => opt.MapFrom(source => source.Features)
                )
                .ReverseMap();

            CreateMap<Domain.ValueObjects.EnrichedProduct, Models.Request.EnrichedProduct>()
                .ReverseMap();
        }
    }
}
