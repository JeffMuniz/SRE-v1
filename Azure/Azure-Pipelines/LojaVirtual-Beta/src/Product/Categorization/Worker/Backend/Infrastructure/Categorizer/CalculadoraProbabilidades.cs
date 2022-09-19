using Product.Categorization.Worker.Backend.Infrastructure.Categorizer.Models;
using System.Collections.Generic;
using System.Linq;

namespace Product.Categorization.Worker.Backend.Infrastructure.Categorizer
{
    public class CalculadoraProbabilidades
    {
        private int NumeroPalavrasDistintas { get; }

        private IEnumerable<Subcategoria> DadosParaAnalise { get; }

        private int TotalProdutosBase { get; }

        public CalculadoraProbabilidades(IEnumerable<Subcategoria> dadosParaAnalise)
        {
            DadosParaAnalise = dadosParaAnalise;
            TotalProdutosBase = DadosParaAnalise
                .SelectMany(a => a.Produtos)
                .Count();

            NumeroPalavrasDistintas = dadosParaAnalise.SelectMany(a => a.Produtos)
                .SelectMany(a => a.Palavras)
                .Distinct()
                .Count();
        }

        /// <summary>
        /// Calcula as probabilidades de cada categoria, com base na nota obtida pela categoria
        /// A nota é proporcional, mas não igual à probabilidade.
        /// </summary>
        /// <param name="notas"></param>
        /// <returns></returns>
        public IOrderedEnumerable<KeyValuePair<int, double>> CalcularVetorDeProbabilidades(ProdutoProcessado produtoCandidato)
        {
            Dictionary<int, double> notas = new Dictionary<int, double>();
            foreach (var subcategoria in DadosParaAnalise)
            {
                var nota = CalcularProbabilidadeFinalDaCategoria(produtoCandidato, subcategoria.IdNumerico);
                notas[subcategoria.IdNumerico] = nota;
            }

            var fatorProbabilistico = notas.Select(a => a.Value).Sum();
            var retorno = new Dictionary<int, double>();
            foreach (var nota in notas)
                retorno[nota.Key] =  nota.Value/fatorProbabilistico;

            return retorno.OrderByDescending(a => a.Value);
        }

        /// <summary>
        /// Probabilidade de a Categoria instanciada conter o produto representado pelo conjunto de palavras recebido
        /// </summary>
        /// <param name="palavras"></param>
        /// <returns></returns>
        private double CalcularProbabilidadeFinalDaCategoria(ProdutoProcessado produto, int IdCategoria)
        {
            //pci representa a probabilidade de um produto qualquer pertencer à categoria em questão
            var Pci = CalcularProbabilidadeDaCategoriaPc(IdCategoria);
            double produtoria = 1;
            foreach (var palavra in produto.Palavras)
            {
                var probabilidadePalavra = ProbabilidadeDeConterPalavra(palavra, IdCategoria);
                produtoria = produtoria * probabilidadePalavra;
            }
            return Pci * produtoria;
        }

        /// <summary>
        /// Probabilidade de um produto qualquer pertencer à categoria instanciada
        /// </summary>
        private double CalcularProbabilidadeDaCategoriaPc(int IdCategoria)
        {
            int k = 1;
            //número de produtos na categoria
            var count = DadosParaAnalise.Where(a => a.IdNumerico == IdCategoria).FirstOrDefault().Produtos.Count();

            //número de categorias possíveis
            var numeroClasses = DadosParaAnalise.Count();
            return (count + k) / (double)(TotalProdutosBase + k * numeroClasses);
        }

        /// <summary>
        /// Calcula a probabilidade de a categoria instanciada conter a palavra recebida. Utiliza-se aqui o método de amortecimento de Laplaceno cálculo da probabilidade.
        /// </summary>
        /// <param name="palavra"></param>
        /// <returns></returns>
        private double ProbabilidadeDeConterPalavra(string palavra, int idCategoria)
        {
            int k = 1;
            var produtosDaCategoria = DadosParaAnalise.Where(a => a.IdNumerico == idCategoria).First().Produtos;
            var produtosQueContemPalavraRecebida = produtosDaCategoria
                .Where(
                    p => p.Palavras.Contains(palavra)
                ).Count();
            var totalProdutosNaCategoria = produtosDaCategoria.Count();
            var retorno = (produtosQueContemPalavraRecebida + k) / (double)(totalProdutosNaCategoria + k * NumeroPalavrasDistintas);
            return retorno;

        }
    }
}
