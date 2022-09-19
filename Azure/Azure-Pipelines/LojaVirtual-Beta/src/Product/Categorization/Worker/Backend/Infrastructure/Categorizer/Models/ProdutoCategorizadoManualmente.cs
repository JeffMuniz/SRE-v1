using System;

namespace Product.Categorization.Worker.Backend.Infrastructure.Categorizer.Models
{
    public class ProdutoCategorizadoManualmente : Produto, ICloneable
    {
        public int IdSubcategoria { get; set; }

        public object Clone()
        {
            return new ProdutoCategorizadoManualmente()
            {
                Caracteristicas = this.Caracteristicas,
                CategoriaParceiro = this.CategoriaParceiro,
                Id = this.Id,
                IdSubcategoria = this.IdSubcategoria,
                Marca = this.Marca,
                Nome = this.Nome,
                SubcategoriaParceiro = this.SubcategoriaParceiro
            };
        }
    }
}
