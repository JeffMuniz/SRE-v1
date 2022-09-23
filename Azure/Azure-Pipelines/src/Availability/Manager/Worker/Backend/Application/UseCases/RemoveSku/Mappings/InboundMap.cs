using AutoMapper;
using SharedValueObjects = Shared.Backend.Domain.ValueObjects;

namespace Availability.Manager.Worker.Backend.Application.UseCases.RemoveSku.Mappings
{
    public class InboundMap : Profile
    {
        public InboundMap()
        {
            CreateMap<Models.Inbound, SharedValueObjects.SupplierSkuId>()
                .ForMember(
                    dest => dest.SkuId,
                    opt => opt.MapFrom(source => source.SupplierSkuId)
                )
                .ReverseMap();
        }
    }
}
