using AutoMapper;

namespace Shared.Backend.Application.Usecases.Mappings
{
    public class SupplierSkuMap : Profile
    {
        public SupplierSkuMap()
        {
            CreateMap<Models.SupplierSku, Domain.ValueObjects.SupplierSkuId>()
                .ForMember(
                    dest => dest.SkuId,
                    opt => opt.MapFrom(source => source.SkuId)
                )
                .ReverseMap();
        }
    }
}
