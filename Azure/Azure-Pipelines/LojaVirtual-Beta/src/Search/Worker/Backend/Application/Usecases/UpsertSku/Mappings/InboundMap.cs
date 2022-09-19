using AutoMapper;
using SharedDomain = Shared.Backend.Domain;
using SharedUsecases = Shared.Backend.Application.Usecases;

namespace Search.Worker.Backend.Application.Usecases.UpsertSku.Mappings
{
    public class InboundMap : Profile
    {
        public InboundMap()
        {
            CreateMap<Models.Inbound, Domain.Entities.Sku>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(source => source)
                )
                .ForMember(
                    dest => dest.SupplierSkuId,
                    opt => opt.MapFrom(source => source)
                )
                .ForPath(
                    dest => dest.Supplier,
                    opt => opt.MapFrom(source => source)
                )
                .ForMember(
                    dest => dest.Relevance,
                    opt => opt.MapFrom(source => source.Entity)
                )
                .ForMember(
                    dest => dest.Features,
                    opt => opt.MapFrom(source => source.SkuFeatures)
                )
                .ForMember(
                    dest => dest.Keywords,
                    opt => opt.MapFrom(source => source.Keywords)
                )
                .ForMember(
                    dest => dest.Type,
                    opt => opt.MapFrom((source, dest) => MapProductType())
                )
                .ForMember(
                    dest => dest.Group,
                    opt => opt.MapFrom((source, dest, member, context) =>
                    {
                        var hashProviderService = context.Options.CreateInstance<Domain.Services.IHashProviderService>();
                        return Domain.ValueObjects.SkuGroupHash.Create(source.ProductId, source.SkuFeatures, hashProviderService);
                    })
                )
                .ForMember(
                    dest => dest.Availability,
                    opt => opt.Ignore()
                )
                .ForMember(
                    dest => dest.ServicePath,
                    opt => opt.Ignore()
                )
                .ForMember(
                    dest => dest.CreatedDate,
                    opt => opt.Ignore()
                );

            CreateMap<Models.Inbound, Domain.ValueObjects.SkuId>()
                .ForMember(
                    dest => dest.Value,
                    opt => opt.MapFrom(source => source.SkuId)
                )
                .ReverseMap();

            CreateMap<Models.Inbound, SharedDomain.ValueObjects.SupplierSkuId>()
                .ForMember(
                    dest => dest.SkuId,
                    opt => opt.MapFrom(source => source.SupplierSkuId)
                )
                .ReverseMap()
                .ForMember(
                    dest => dest.SkuId,
                    opt => opt.Ignore()
                );

            CreateMap<Models.Inbound, Domain.Entities.Supplier>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(source => source.SupplierId)
                )
                .ForMember(
                    dest => dest.Name,
                    opt => opt.MapFrom(source => source.SupplierName)
                )
                .ForMember(
                    dest => dest.Type,
                    opt => opt.MapFrom((source, dest) => MapSupplierType(source.SupplierTypeId))
                );

            CreateMap<SharedUsecases.Models.Brand, Domain.Entities.Brand>()
                .ReverseMap();

            CreateMap<SharedUsecases.Models.Category, Domain.Entities.Category>()
                .ReverseMap();

            CreateMap<SharedUsecases.Models.Subcategory, Domain.Entities.Subcategory>()
                .ReverseMap();
        }

        private static Domain.ValueObjects.SupplierType MapSupplierType(string supplierTypeId) =>
            Domain.ValueObjects.SupplierType
                .FromId(supplierTypeId)
                .GetValueOrDefault();

        private static Domain.ValueObjects.ProductType MapProductType() =>
            Domain.ValueObjects.ProductType.Product;
    }
}
