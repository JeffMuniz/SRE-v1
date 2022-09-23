using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using SharedUsecaseModels = Shared.Backend.Application.Usecases.Models;

namespace Product.Persistence.Worker.Backend.Application.Usecases.UpsertSku.Mappings
{
    public class InboundMap : Profile
    {
        public InboundMap()
        {
            CreateMap<Models.Inbound, Domain.Entities.Product>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(source => source.SupplierSku.ProductId)
                )
                .ForMember(
                    dest => dest.SupplierId,
                    opt => opt.MapFrom(source => source.SupplierSku.SupplierId)
                )
                .ForMember(
                    dest => dest.Name,
                    opt => opt.MapFrom((source, dest) => source.EnrichedData?.Product?.Name ?? source.SupplierSku.Name)
                )
                .ForMember(
                    dest => dest.Description,
                    opt => opt.MapFrom(source => source.SupplierSku.Description)
                )
                .ForMember(dest => dest.Keywords,
                    opt => opt.Ignore()
                )
                .ForMember(
                    dest => dest.Features,
                    opt => opt.MapFrom((source, dest) => MapProductFeatures(source.SupplierSku.Attributes, source.EnrichedData?.Product?.Attributes))
                )
                .ForMember(
                    dest => dest.Sections,
                    opt => opt.MapFrom(source => MapSections(source.SupplierSku))
                )
                .ForMember(
                    dest => dest.EnrichedProductAttributes,
                    opt =>
                    {
                        opt.PreCondition(source => source.EnrichedData?.Product?.Attributes != null);
                        opt.MapFrom(source => source.EnrichedData.Product.Attributes);
                    }
                )
                .ForMember(
                    dest => dest.SubcategoryId,
                    opt => opt.MapFrom(source => MapSubcategory(source))
                )
                .ForMember(
                    dest => dest.EnrichedProduct,
                    opt =>
                    {
                        opt.PreCondition(source => source.EnrichedData?.Product != null);
                        opt.MapFrom((source, dest) => new Domain.ValueObjects.EnrichedProduct
                        {
                            Entity = source.EnrichedData.Product?.Entity,
                            Hash = source.EnrichedData.Product?.Hash,
                            Name = source.EnrichedData.Product?.Name
                        });
                    }
                )
                .ForMember(
                    dest => dest.Sku,
                    opt => opt.MapFrom(source => source)
                )
                .ReverseMap();

            CreateMap<Models.Inbound, Domain.Entities.ProductSku>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(source => source.SupplierSku.SkuId)
                )
                .ForMember(
                    dest => dest.ProductId,
                    opt => opt.MapFrom(source => source.SupplierSku.ProductId)
                )
                .ForMember(
                    dest => dest.SkuStatus,
                    opt => opt.MapFrom(
                        source => source.SupplierSku.Active
                            ? Domain.ValueObjects.SkuStatus.Available
                            : Domain.ValueObjects.SkuStatus.Unavailable
                    )
                )
                .ForMember(
                    dest => dest.Ean,
                    opt => opt.MapFrom(source => source.SupplierSku.Ean)
                )
                .ForMember(
                    dest => dest.PriceFrom,
                     opt =>
                     {
                         opt.PreCondition(source => source.SupplierSku.Price != null);
                         opt.MapFrom(source => source.SupplierSku.Price.From);
                     }
                )
                .ForMember(
                    dest => dest.PriceFor,
                     opt =>
                     {
                         opt.PreCondition(source => source.SupplierSku.Price != null);
                         opt.MapFrom(source => source.SupplierSku.Price.For);
                     }
                )
                .ForMember(
                    dest => dest.Description,
                    opt => opt.MapFrom(source => source.SupplierSku.Description)
                )
                .ForMember(
                    dest => dest.SkuFeatures,
                    opt => opt.MapFrom((source, dest) => MapSkuFeatures(source.SupplierSku.Attributes, source.EnrichedData?.Sku?.Attributes))
                )
                .ForMember(
                    dest => dest.SkuImages,
                    opt => opt.MapFrom((source, dest) => MapImages(source.SupplierSku.Images))
                )
                .ForMember(
                    dest => dest.SupplierSkuAttributes,
                    opt => opt.MapFrom(source => source.SupplierSku.Attributes)
                )
                .ForMember(
                    dest => dest.EnrichedSkuAttributes,
                    opt =>
                    {
                        opt.PreCondition(source => source.EnrichedData?.Sku?.Attributes != null);
                        opt.MapFrom(source => source.EnrichedData.Sku.Attributes);
                    }
                )
                .ForMember(
                    dest => dest.EnrichedSku,
                    opt =>
                    {
                        opt.PreCondition(source => source.EnrichedData?.Sku != null);
                        opt.MapFrom((source, desst) => new Domain.ValueObjects.EnrichedSku
                        {
                            Hash = source.EnrichedData.Sku?.Hash,
                            Name = source.EnrichedData.Sku?.Name
                        });
                    }
                )
                .ReverseMap();
        }

        private static int? MapSubcategory(Models.Inbound source)
        {
            if (source.EnrichedData?.Product?.SubcategoryId is int enrichedSubcategoryId)
                return enrichedSubcategoryId;

            return source.CategorizationData?.SubcategoryId;
        }

        private static IEnumerable<Domain.ValueObjects.SkuImage> MapImages(IEnumerable<SharedUsecaseModels.Image> images)
        {
            var mappedImages = images
                .SelectMany(image =>
                    new[]
                    {
                        new Domain.ValueObjects.SkuImage
                        {
                            Order = image.Order,
                            SmallImage = image.Sizes.FirstOrDefault(i => i.Size.ToLower() == "small")?.Url?.AbsoluteUri,
                            MediumImage = image.Sizes.FirstOrDefault(i => i.Size.ToLower() == "medium")?.Url?.AbsoluteUri,
                            LargeImage = image.Sizes.FirstOrDefault(i => i.Size.ToLower() == "large")?.Url?.AbsoluteUri
                        }
                    }
                );
            return mappedImages;
        }

        private static IEnumerable<Domain.Entities.Section> MapSections(SharedUsecaseModels.SupplierSku supplierSku)
        {
            var sections = new List<Domain.Entities.Section>();

            if (!string.IsNullOrWhiteSpace(supplierSku.Brand?.Id))
                sections.Add(Domain.Entities.Section.CreateBrand(supplierSku.Brand.Id, supplierSku.Brand.Name));

            if (!string.IsNullOrWhiteSpace(supplierSku.Subcategory?.Category?.Id))
            {
                var categorySection = Domain.Entities.Section.CreateCategory(supplierSku.Subcategory.Category.Id, supplierSku.Subcategory.Category.Name);
                sections.Add(categorySection);

                if (!string.IsNullOrWhiteSpace(supplierSku.Subcategory?.Id))
                    sections.Add(Domain.Entities.Section.CreateSubcategory(categorySection, supplierSku.Subcategory.Id, supplierSku.Subcategory.Name));
            }

            return sections;
        }

        private static IEnumerable<Domain.ValueObjects.Feature> MapProductFeatures(
            IDictionary<string, string> supplierAttributes,
            IDictionary<string, string> enrichedProductAttributes
        )
        {
            var modelFeatures = SelectFeatures(enrichedProductAttributes, Domain.ValueObjects.FeatureType.Model);
            if (modelFeatures.Empty())
                modelFeatures = SelectFeatures(supplierAttributes, Domain.ValueObjects.FeatureType.Model);

            var features = modelFeatures
                .Concat(SelectProductFeatures(supplierAttributes, Domain.ValueObjects.FeatureType.GeneralFeature))
                .Concat(SelectProductFeatures(enrichedProductAttributes, Domain.ValueObjects.FeatureType.TechnicalSpecification));

            return features;
        }

        private static IEnumerable<Domain.ValueObjects.Feature> SelectProductFeatures(
                IDictionary<string, string> attributes,
                Domain.ValueObjects.FeatureType featureType
            )
        {
            var excludedFeatureType = Domain.ValueObjects.FeatureType.Color.Synonyms
                .Concat(Domain.ValueObjects.FeatureType.Size.Synonyms)
                .Concat(Domain.ValueObjects.FeatureType.Voltage.Synonyms)
                .Concat(Domain.ValueObjects.FeatureType.Model.Synonyms);

            return attributes
                .DefaultIfNull()
                .Where(a => !excludedFeatureType.Contains(a.Key.NormalizeCompare()))
                .Select(a => new Domain.ValueObjects.Feature
                {
                    FeatureType = featureType,
                    Name = a.Key,
                    Value = a.Value
                });
        }

        private static IEnumerable<Domain.ValueObjects.Feature> MapSkuFeatures(
            IDictionary<string, string> supplierAttributes,
            IDictionary<string, string> enrichedSkuAttributes
        )
        {
            var features = SelectSkuFeatures(enrichedSkuAttributes);

            if (features.Empty())
                features = SelectSkuFeatures(supplierAttributes);

            return features;
        }

        private static IEnumerable<Domain.ValueObjects.Feature> SelectSkuFeatures(
            IDictionary<string, string> attributes
        ) =>
            SelectFeatures(attributes, Domain.ValueObjects.FeatureType.Color)
                .Concat(SelectFeatures(attributes, Domain.ValueObjects.FeatureType.Size))
                .Concat(SelectFeatures(attributes, Domain.ValueObjects.FeatureType.Voltage));

        private static IEnumerable<Domain.ValueObjects.Feature> SelectFeatures(
            IDictionary<string, string> attributes,
            Domain.ValueObjects.FeatureType featureType
        ) =>
            attributes
                .DefaultIfNull()
                .Where(a => featureType.Synonyms.Contains(a.Key.NormalizeCompare()))
                .Select(a => new Domain.ValueObjects.Feature
                {
                    FeatureType = featureType,
                    Name = a.Key,
                    Value = a.Value
                });
    }
}
