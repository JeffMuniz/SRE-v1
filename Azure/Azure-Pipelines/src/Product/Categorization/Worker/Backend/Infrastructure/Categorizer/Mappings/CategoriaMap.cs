using AutoMapper;
using System.Linq;

namespace Product.Categorization.Worker.Backend.Infrastructure.Categorizer.Mappings
{
    public class CategoriaMap : Profile
    {
        public CategoriaMap()
        {
            CreateMap<Domain.Entities.Category, Models.Categoria>()
                .ForMember(
                    dest => dest.IdNumerico,
                    opt => opt.MapFrom(source => source.Id)
                )
                .ForMember(
                    dest => dest.Nome,
                    opt => opt.MapFrom(source => source.Name)
                )
                .ForMember(
                    dest => dest.Subcategorias,
                    opt => opt.MapFrom(source => source.Subcategories)
                );
        }
    }
}
