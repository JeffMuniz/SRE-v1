using AutoMapper;

namespace Product.Categorization.Worker.Backend.Application.Usecases.CategorizeSku.Mappings
{
    public class InboundMap : Profile
    {
        public InboundMap()
        {
            CreateMap<Models.Inbound, Domain.Entities.Product>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(source => source.SkuIntegrationId)
                )
                .ForMember(
                    dest => dest.Name,
                    opt => opt.MapFrom(source => source.SupplierSku.Name)
                )
                .ForMember(
                    dest => dest.Brand,
                    opt => opt.MapFrom(source => source.SupplierSku.Brand.Name)
                )
                .ForMember(
                    dest => dest.PartnerCategory,
                    opt => opt.MapFrom(source => source.SupplierSku.Subcategory.Category.Name)
                )
                .ForMember(
                    dest => dest.PartnerSubcategory,
                    opt => opt.MapFrom(source => source.SupplierSku.Subcategory.Name)
                )
                .ForMember(
                    dest => dest.Features,
                    opt => opt.MapFrom(source => source.SupplierSku.Attributes)
                )
                .ReverseMap();
        }
    }
}
