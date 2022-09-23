using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using SharedDomain = Shared.Backend.Domain;
using SharedSupplierDomain = Product.Supplier.Shared.Worker.Backend.Domain;

namespace Product.Supplier.Magalu.Worker.Backend.Infrastructure.ExternalServices.Magalu.Mappings
{
    public class SkuMap : Profile
    {
        public SkuMap()
        {
            CreateMap<Models.Sku, Domain.Entities.Sku>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(source => source)
                )
                .ForMember(
                    dest => dest.Subcategory,
                    opt => opt.MapFrom(source => source)
                )
                .ForMember(
                    dest => dest.Voltage,
                    opt => opt.MapFrom(source => MapVoltage(source.Voltage))
                )
                .ForMember(
                    dest => dest.Price,
                     opt =>
                     {
                         opt.PreCondition(source => source.Price.HasValue || source.SellingPrice.HasValue);
                         opt.MapFrom(source => source);
                     }
                )
                .ForMember(
                    dest => dest.Images,
                    opt => opt.MapFrom(source => MapImages(source))
                )
                .ReverseMap();

            CreateMap<Models.Sku, Domain.ValueObjects.SkuId>()
                .ReverseMap();

            CreateMap<Models.Sku, SharedSupplierDomain.Entities.Subcategory>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(source => source.Subcategory)
                )
                .ForMember(
                    dest => dest.Name,
                    opt => opt.MapFrom(source => source.SubcategoryDescription)
                )
                .ForMember(
                    dest => dest.Category,
                    opt => opt.MapFrom(source => source)
                )
                .ReverseMap();

            CreateMap<Models.Sku, SharedSupplierDomain.Entities.Category>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(source => source.Category)
                )
                .ForMember(
                    dest => dest.Name,
                    opt => opt.MapFrom(source => source.CategoryDescription)
                )
                .ReverseMap();

            CreateMap<Models.Sku, SharedDomain.ValueObjects.Price>()
                .ForMember(
                    dest => dest.From,
                    opt => opt.MapFrom(source => source.Price.GetValueOrDefault())
                )
                .ForMember(
                    dest => dest.For,
                    opt => opt.MapFrom(source => source.SellingPrice.GetValueOrDefault())
                )
                .ReverseMap();
        }

        private static Domain.ValueObjects.Voltage MapVoltage(int voltage) =>
            Domain.ValueObjects.Voltage.FromId(voltage.ToString()).GetValueOrDefault();

        private static IEnumerable<SharedDomain.ValueObjects.Image> MapImages(Models.Sku source) =>
            source.Images
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select((imageUrl, index) =>
                    new SharedDomain.ValueObjects.Image
                    {
                        Order = index + 1,
                        Sizes = SharedDomain.ValueObjects.ImageSizeType
                            .All
                            .Select(imageSize => new SharedDomain.ValueObjects.ImageSize
                            {
                                Size = imageSize,
                                Url = new Uri(imageUrl.Replace("{w}x{h}", $"{imageSize.Width}x{imageSize.Height}"))
                            })
                    }
                );
    }
}
