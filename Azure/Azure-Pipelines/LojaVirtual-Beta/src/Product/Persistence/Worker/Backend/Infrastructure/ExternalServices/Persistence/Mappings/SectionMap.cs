using AutoMapper;
using Product.Persistence.Worker.Backend.Infrastructure.ExternalServices.Persistence.Models.Request;

namespace Product.Persistence.Worker.Backend.Infrastructure.ExternalServices.Persistence.Mappings
{
    public class SectionMap : Profile
    {
        public SectionMap()
        {
            CreateMap<Domain.Entities.Section, Section>()
                .ForMember(
                    dest => dest.SectionId,
                    opt => opt.MapFrom(source => source.Id)
                )
                .ForMember(
                    dest => dest.SectionTypeId,
                    opt => opt.MapFrom(source => source.SectionType.ToInteger())
                )
                .ForMember(
                    dest => dest.SectionParentId,
                    opt => opt.MapFrom((source, dest) => source.Parent?.Id)
                )
                .ReverseMap();
        }
    }
}
