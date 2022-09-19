using AutoMapper;
using Product.Persistence.Worker.Backend.Infrastructure.ExternalServices.Persistence.Models.Request;

namespace Product.Persistence.Worker.Backend.Infrastructure.ExternalServices.Persistence.Mappings
{
    public class FeatureMap : Profile
    {
        public FeatureMap()
        {
            CreateMap<Domain.ValueObjects.Feature, Feature>()
                .ForMember(
                    dest => dest.FeatureType,
                    opt => opt.MapFrom(source => source.FeatureType.ToInteger())
                )
                .ReverseMap();
        }
    }
}
