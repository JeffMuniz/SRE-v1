using AutoMapper;
using Product.Supplier.Magalu.Worker.Backend.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using SharedDomain = Shared.Backend.Domain;
using SharedSupplierDomain = Product.Supplier.Shared.Worker.Backend.Domain;

namespace Product.Supplier.Magalu.Worker.Backend.Application.Usecases.IntegrateFullCatalog.Mappings
{
    public class SupplierSkuMap : Profile
    {
        public SupplierSkuMap()
        {
            CreateMap<Domain.Entities.Sku, SharedSupplierDomain.Entities.SupplierSku>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom((source, dest, member, context) =>
                    {
                        if (context.Items.TryGetValue("SupplierId", out var contextValue) && contextValue is int supplierId)
                            return new SharedDomain.ValueObjects.SupplierSkuId { SupplierId = supplierId, SkuId = source.Id.ToString() };

                        return new SharedDomain.ValueObjects.SupplierSkuId { SkuId = source.Id.ToString() };
                    })
                )
                .ForMember(
                    dest => dest.ProductId,
                    opt => opt.MapFrom(source => source.Master)
                )
                .ForMember(
                    dest => dest.Name,
                    opt => opt.MapFrom(source => $"{source.Description} {source.Reference}".Trim())
                )
                .ForMember(
                    dest => dest.Description,
                    opt => opt.MapFrom(source => MapDescription(source))
                )
                .ForMember(
                    dest => dest.Brand,
                    opt => opt.MapFrom(source => source)
                )
                .ForMember(
                    dest => dest.Attributes,
                    opt => opt.MapFrom(source => MapAttributes(source))
                )
                .ForMember(
                    dest => dest.Images,
                    opt => opt.MapFrom(source => source.Images)
                )
                .ForMember(
                    dest => dest.Active,
                    opt => opt.MapFrom(source => source.Active.GetValueOrDefault())
                )
                .ReverseMap();

            CreateMap<Domain.Entities.Sku, SharedSupplierDomain.Entities.Brand>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(source => source.Brand.NormalizeCompare())
                )
                .ForMember(
                    dest => dest.Name,
                    opt => opt.MapFrom(source => source.Brand)
                )
                .ReverseMap();
        }

        private static IDictionary<string, string> MapAttributes(Domain.Entities.Sku source)
        {
            var attributes = source
                .Specifications
                .GroupBy(spec => spec.Name)
                .ToDictionary(
                    group => group.Key,
                    group => string.Join(", ", group.Select(spec => spec.Value))
                );

            if (source.Color?.Id != "00" && !string.IsNullOrWhiteSpace(source.Color?.Id))
                attributes["Cor"] = source.Color.Name;

            if (source.Voltage != null && source.Voltage?.Id != Voltage.None)
                attributes["Voltagem"] = source.Voltage.Description;

            return attributes;
        }

        private static string MapDescription(Domain.Entities.Sku source)
        {
            if (string.IsNullOrWhiteSpace(source.DescriptionSpecification))
                return source.Description;

            return source.DescriptionSpecification;
        }
    }
}
