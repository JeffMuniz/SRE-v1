using AutoMapper;

namespace Search.Worker.Backend.Application.Usecases.UpdateSkuAvailability.Mappings
{
    public class InboundMap : Profile
    {
        public InboundMap()
        {
            CreateMap<Models.Inbound, Domain.ValueObjects.SkuId>()
                .ForMember(
                    dest => dest.Value,
                    opt => opt.MapFrom(source => source.PersistedSkuId)
                )
                .ReverseMap();

            CreateMap<Models.Inbound, Domain.ValueObjects.Availability>()
                .ReverseMap();
        }
    }
}
