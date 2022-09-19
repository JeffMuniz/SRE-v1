using AutoMapper;

namespace Product.Categorization.Worker.Backend.Infrastructure.Persistence.KnowledgeData.Mappings
{
    public class CategoriaMap : Profile
    {
        public CategoriaMap()
        {
            CreateMap<Entities.Categoria, Domain.Entities.Category>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(source => source.IdNumerico)
                )
                .ForMember(
                    dest => dest.Name,
                    opt => opt.MapFrom(source => source.Nome)
                )
                .ReverseMap();
        }
    }
}
