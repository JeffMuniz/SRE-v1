using AutoMapper;
using System.Collections.Generic;

namespace Product.Categorization.Worker.Backend.Infrastructure.Categorizer.Mappings
{
    public class ProbabilidadeMap : Profile
    {
        public ProbabilidadeMap()
        {
            CreateMap<KeyValuePair<int, double>, Domain.ValueObjects.SubcategoryProbability>()
                .ForMember(
                    dest => dest.SubcategoryId,
                    opt => opt.MapFrom(source => source.Key)
                )
                .ForMember(
                    dest => dest.Probability,
                    opt => opt.MapFrom(source => source.Value)
                );
        }
    }
}
