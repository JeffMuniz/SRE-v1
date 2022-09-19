using AutoMapper;
using SharedUsecases = Shared.Backend.Application.Usecases;
using SharedDomain = Shared.Backend.Domain;

namespace Product.Change.Worker.Backend.Application.Usecases.GetSkuDetail.Mappings
{
    public class OutboundMap : Profile
    {
        public OutboundMap()
        {
            CreateMap<Models.Outbound, Domain.Entities.SkuIntegration>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(source => source.SkuIntegrationId)
                )
                .ForMember(
                    dest => dest.SupplierSku,
                    opt => opt.MapFrom(source => source)
                )
                .ReverseMap();

            CreateMap<Models.Outbound, Domain.Entities.SupplierSku>()
                .IncludeBase<SharedUsecases.Models.SupplierSku, Domain.Entities.SupplierSku>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(source => source)
                )
                .ReverseMap();

            CreateMap<Models.Outbound, SharedDomain.ValueObjects.SupplierSkuId>()
                .ReverseMap();
        }
    }
}
