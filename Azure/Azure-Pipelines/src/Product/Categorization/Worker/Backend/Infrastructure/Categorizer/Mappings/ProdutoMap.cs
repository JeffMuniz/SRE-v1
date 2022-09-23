using AutoMapper;
using System.Linq;

namespace Product.Categorization.Worker.Backend.Infrastructure.Categorizer.Mappings
{
    public class ProdutoMap : Profile
    {
        public ProdutoMap()
        {
            CreateMap<Domain.Entities.Product, Models.Produto>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(source => source.Id)
                )
                .ForMember(
                    dest => dest.Nome,
                    opt => opt.MapFrom(source => source.Name)
                )
                .ForMember(
                    dest => dest.Marca,
                    opt => opt.MapFrom(source => source.Brand)
                )
                .ForMember(
                    dest => dest.CategoriaParceiro,
                    opt => opt.MapFrom(source => source.PartnerCategory)
                )
                .ForMember(
                    dest => dest.SubcategoriaParceiro,
                    opt => opt.MapFrom(source => source.PartnerSubcategory)
                )
                .ForMember(
                    dest => dest.Caracteristicas,
                    opt => opt.MapFrom(source => source.Features
                        .Select(x => new Models.Caracteristica
                        {
                            ProductId = source.Id,
                            Nome = x.Key,
                            Valor = x.Value
                        })
                    )
                );
        }
    }
}
