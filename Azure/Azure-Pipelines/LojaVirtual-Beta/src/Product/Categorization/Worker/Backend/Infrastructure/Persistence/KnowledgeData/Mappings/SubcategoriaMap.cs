using AutoMapper;
using System.Collections.Generic;
using System.Linq;

namespace Product.Categorization.Worker.Backend.Infrastructure.Persistence.KnowledgeData.Mappings
{
    public class SubcategoriaMap : Profile
    {
        public SubcategoriaMap()
        {
            CreateMap<KeyValuePair<Entities.Categoria, IEnumerable<Entities.Subcategoria>>, Domain.Entities.Category>()
                .IncludeMembers(source => source.Key)
                .ForMember(
                    dest => dest.Subcategories,
                    opt => opt.MapFrom(source => source.Value)
                );

            CreateMap<Entities.Subcategoria, Domain.Entities.Subcategory>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(source => source.IdNumerico)
                )
                .ForMember(
                    dest => dest.Name,
                    opt => opt.MapFrom(source => source.Nome)
                )
                .ForPath(
                    dest => dest.Category.Id,
                    opt => opt.MapFrom(source => source.IdNumericoCategoria)
                );
        }
    }
}
