using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using SharedDomain = Shared.Backend.Domain;

namespace Search.Worker.Backend.Infrastructure.Persistence.Mappings
{
    public class SkuMap : Profile
    {
        public SkuMap()
        {
            CreateMap<Domain.Entities.Sku, Domain.Entities.Sku>()
                .ForMember(
                    dest => dest.Availability,
                    opt => opt.PreCondition(source => source.Availability != null)
                )
                .ForMember(
                    dest => dest.CreatedDate,
                    opt => opt.Ignore()
                );

            CreateMap<Models.SearchIndexModel, Domain.Entities.Sku>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(source => source)
                )
                .ForMember(
                    dest => dest.SupplierSkuId,
                    opt => opt.MapFrom(source => source)
                )
                .ForMember(
                    dest => dest.Supplier,
                    opt => opt.MapFrom(source => source)
                )
                .ForMember(
                    dest => dest.Brand,
                    opt => opt.MapFrom(source => source)
                )
                .ForMember(
                    dest => dest.Subcategory,
                    opt => opt.MapFrom(source => source)
                )
                .ForMember(
                    dest => dest.Image,
                    opt => opt.MapFrom(source => source)
                )
                .ForMember(
                    dest => dest.Availability,
                    opt => opt.MapFrom(source => source)
                )
                .ForMember(
                    dest => dest.Group,
                    opt => opt.MapFrom(source => source.Group)
                )
                .ForMember(
                    dest => dest.Keywords,
                    opt => opt.MapFrom(source => MapKeywords(source.KeyWord))
                )
                .ForMember(
                    dest => dest.Type,
                    opt => opt.MapFrom(source => MapProductType(source.Type))
                )
                .ForMember(
                    dest => dest.Features,
                    opt => opt.MapFrom(source => MapFeatures(source.Features))
                )
                .ReverseMap()
                .ForMember(
                    dest => dest.Category,
                    opt => opt.MapFrom(source => source.Subcategory.Category.Name)
                )
                .ForMember(
                    dest => dest.Subcategory,
                    opt => opt.MapFrom(source => source.Subcategory.Name)
                )
                .ForMember(
                    dest => dest.Brand,
                    opt => opt.MapFrom(source => source.Brand.Name)
                )
                .ForMember(
                    dest => dest.Image,
                    opt => opt.MapFrom(source => source.Image.Url)
                )
                .ForMember(
                    dest => dest.KeyWord,
                    opt => opt.MapFrom(source => MapKeywords(source.Keywords))
                )
                .ForMember(
                    dest => dest.Type,
                    opt => opt.MapFrom(source => MapProductType(source.Type))
                )
                .ForMember(
                    dest => dest.Features,
                    opt => opt.MapFrom(source => MapFeatures(source.Features))
                );

            CreateMap<Models.SearchIndexModel, Domain.ValueObjects.SkuId>()
                .ForMember(
                    dest => dest.Value,
                    opt => opt.MapFrom(source => source.ProductSkuId)
                )
                .ReverseMap();

            CreateMap<Models.SearchIndexModel, SharedDomain.ValueObjects.SupplierSkuId>()
                .ForMember(
                    dest => dest.SupplierId,
                    opt => opt.MapFrom(source => source.PartnerId)
                )
                .ForMember(
                    dest => dest.SkuId,
                    opt => opt.MapFrom(source => source.OriginalProductSkuId)
                )
                .ReverseMap();

            CreateMap<Models.SearchIndexModel, Domain.Entities.Supplier>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(source => source.PartnerId)
                )
                .ForMember(
                    dest => dest.Name,
                    opt => opt.MapFrom(source => source.Partner)
                )
                .ForMember(
                    dest => dest.Type,
                    opt => opt.MapFrom(source => MapSupplierType(source.PartnerType))
                )
                .ReverseMap()
                .ForMember(
                    dest => dest.PartnerId,
                    opt => opt.MapFrom(source => source.Id)
                )
                .ForMember(
                    dest => dest.Partner,
                    opt => opt.MapFrom(source => source.Name)
                )
                .ForMember(
                    dest => dest.PartnerType,
                    opt => opt.MapFrom(source => MapSupplierType(source.Type))
                );

            CreateMap<Models.SearchIndexModel, Domain.Entities.Brand>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(source => source.BrandId)
                )
                .ForMember(
                    dest => dest.Name,
                    opt => opt.MapFrom(source => source.Brand)
                )
                .ReverseMap();

            CreateMap<Models.SearchIndexModel, Domain.Entities.Subcategory>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(source => source.SubCategoryId)
                )
                .ForMember(
                    dest => dest.Name,
                    opt => opt.MapFrom(source => source.Subcategory)
                )
                .ForMember(
                    dest => dest.Category,
                    opt => opt.MapFrom(source => source)
                )
                .ReverseMap();

            CreateMap<Models.SearchIndexModel, Domain.Entities.Category>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(source => source.CategoryId)
                )
                .ForMember(
                    dest => dest.Name,
                    opt => opt.MapFrom(source => source.Category)
                )
                .ReverseMap();

            CreateMap<Models.SearchIndexModel, SharedDomain.ValueObjects.ImageSize>()
                .ForMember(
                    dest => dest.Size,
                    opt => opt.MapFrom((source, dest) => SharedDomain.ValueObjects.ImageSizeType.Small)
                )
                .ForMember(
                    dest => dest.Url,
                    opt => opt.MapFrom(source => source.Image)
                )
                .ReverseMap();

            CreateMap<Models.SearchIndexModel, Domain.ValueObjects.Availability>()
                .ForMember(
                    dest => dest.Available,
                    opt => opt.MapFrom(source => source.Available)
                )
                .ForMember(
                    dest => dest.Price,
                    opt => opt.MapFrom(source => source)
                )
                .ReverseMap();

            CreateMap<Models.SearchIndexModel, SharedDomain.ValueObjects.Price>()
                .ForMember(
                    dest => dest.From,
                    opt => opt.Ignore()
                )
                .ForMember(
                    dest => dest.For,
                    opt => opt.MapFrom(source => source.PriceFor)
                )
                .ReverseMap();
        }

        private static string MapKeywords(IEnumerable<string> keywords) =>
            string.Join(",", keywords);

        private static IEnumerable<string> MapKeywords(string keywords) =>
            keywords.Split(",");

        private static Domain.ValueObjects.ProductType MapProductType(int productTypeId) =>
            Domain.ValueObjects.ProductType
                .FromId(productTypeId.ToString())
                .GetValueOrDefault();

        private static int MapProductType(Domain.ValueObjects.ProductType productType) =>
            productType.ToInteger();

        private static Domain.ValueObjects.SupplierType MapSupplierType(int partnerTypeId) =>
            Domain.ValueObjects.SupplierType
                .FromId(partnerTypeId.ToString())
                .GetValueOrDefault();

        private static int MapSupplierType(Domain.ValueObjects.SupplierType supplierType) =>
            supplierType.ToInteger();

        private IDictionary<string, string> MapFeatures(IEnumerable<string> features) =>
            features
                .Select(feature => feature.Split("="))
                .ToDictionary(
                    x => x.First(),
                    x => x.Skip(1).First()
                );

        private string[] MapFeatures(IDictionary<string, string> features) =>
            features
                .Select(feature => $"{feature.Key}={feature.Value}")
                .AsArray();
    }
}
