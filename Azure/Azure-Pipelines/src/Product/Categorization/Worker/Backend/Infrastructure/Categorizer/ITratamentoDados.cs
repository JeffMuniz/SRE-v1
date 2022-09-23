using Product.Categorization.Worker.Backend.Infrastructure.Categorizer.Models;
using System.Collections.Generic;

namespace Product.Categorization.Worker.Backend.Infrastructure.Categorizer
{
    public interface ITratamentoDados
    {


        /// <summary>
        /// Processa um produto, transformando-o em um ProdutoProcessado, que essencialmente é um grupo de palavras tratadas. 
        /// </summary>
        /// <param name="produtosNaoProcessados"></param>
        /// <returns></returns>
        ProdutoProcessado ProcessarProduto(Produto produtosNaoProcessados);


        /// <summary>
        /// Recebe uma lista de produtos categorizados manualmente, processa individualmente os produtos, fazendo tratamento de palavras e os agrupa nas subcategorias que serão retornadas.
        /// </summary>
        /// <param name="baseDeConhecimento"></param>
        /// <returns></returns>
        IEnumerable<Subcategoria> ProcessarESepararProdutosCategorizados(IEnumerable<ProdutoCategorizadoManualmente> baseDeConhecimento, IEnumerable<Categoria> categoriasUtilizadas);


    }
}
