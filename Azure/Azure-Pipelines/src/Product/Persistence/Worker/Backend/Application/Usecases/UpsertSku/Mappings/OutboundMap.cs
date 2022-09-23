using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using SharedUsecaseModels = Shared.Backend.Application.Usecases.Models;

namespace Product.Persistence.Worker.Backend.Application.Usecases.UpsertSku.Mappings
{
    public class OutboundMap : Profile
    {
        public OutboundMap()
        {
            CreateMap<Domain.Entities.Product, Models.Outbound>()
               .IncludeMembers(
                   source => source.EnrichedProduct
               )
               .ForMember(
                   dest => dest.Description,
                   opt => opt.MapFrom(source => source.Description)
               )
               .ForMember(
                   dest => dest.Keywords,
                   opt => opt.MapFrom((source, dest) => MapKeywords(source))
               )
               .ForMember(
                    dest => dest.Ean,
                    opt => opt.MapFrom(source => source.Sku.Ean)
                )
               .ForMember(
                    dest => dest.SkuFeatures,
                    opt => opt.MapFrom((source, dest) => MapSkuFeatures(source.Sku))
                );

            CreateMap<Domain.ValueObjects.EnrichedProduct, Models.Outbound>()
                .ForMember(
                    dest => dest.Entity,
                    opt => opt.MapFrom(source => source.Entity)
                );

            CreateMap<Domain.ValueObjects.PersistedData, Models.Outbound>()
                .IncludeMembers(source => source.Supplier)
                .ForMember(
                    dest => dest.Name,
                    opt => opt.MapFrom(source => source.SkuName)
                );

            CreateMap<Domain.Entities.Brand, SharedUsecaseModels.Brand>()
                .ReverseMap();

            CreateMap<Domain.Entities.Category, SharedUsecaseModels.Category>()
                .ReverseMap();

            CreateMap<Domain.Entities.Subcategory, SharedUsecaseModels.Subcategory>()
                .ReverseMap();

            CreateMap<Domain.ValueObjects.Supplier, Models.Outbound>()
                .ForMember(
                    dest => dest.SupplierName,
                    opt => opt.MapFrom(source => source.Name)
                )
                .ForMember(
                    dest => dest.SupplierTypeId,
                    opt => opt.MapFrom(source => source.TypeId)
                );
        }

        public IEnumerable<string> MapKeywords(Domain.Entities.Product product) =>
            product.Keywords?.Split(",") ?? Enumerable.Empty<string>();

        public IDictionary<string, string> MapSkuFeatures(Domain.Entities.ProductSku sku) =>
            sku.SkuFeatures
                .ToDictionary(
                    x => x.Name,
                    x => x.Value
                );
    }
}
