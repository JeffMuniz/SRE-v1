using AutoMapper;

namespace Product.Change.Worker.Backend.Infrastructure.Persistence.Integration.Mappings
{
    public class SkuIntegrationMap : Profile
    {
        public SkuIntegrationMap()
        {
            CreateMap<Domain.Entities.SkuIntegration, Entities.SkuIntegration>()
                .ForMember(
                    dest => dest.ChangedHash,
                    opt => opt.MapFrom(source => source.ChangedHash)
                )
                .ReverseMap();
        }
    }
}
