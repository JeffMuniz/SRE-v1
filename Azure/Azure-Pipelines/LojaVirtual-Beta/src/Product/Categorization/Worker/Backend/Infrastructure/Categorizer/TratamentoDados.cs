using Product.Categorization.Worker.Backend.Infrastructure.Categorizer.Models;
using System.Collections.Generic;
using System.Linq;

namespace Product.Categorization.Worker.Backend.Infrastructure.Categorizer
{
    public class TratamentoDados : ITratamentoDados
    {
        private static readonly string[] Stopwords = new[] {
            " de ",
            " da ",
            " do ",
            " com ",
            " para ",
            " a ",
            " à ",
            " á ",
            " ã ",
            " é ",
            " è ",
            " ê ",
            " e ",
            " i ",
            " í ",
            " ì ",
            " o ",
            " ó ",
            " ò ",
            " u ",
            " ú ",
            " ù ",
            " não ",
            " & ",
            " - ",
            " – "
        };

        private static readonly Dictionary<char, char> Acentuacao = new()
        {
            ['à'] = 'a',
            ['á'] = 'a',
            ['â'] = 'a',
            ['ã'] = 'a',
            ['é'] = 'e',
            ['è'] = 'e',
            ['ê'] = 'e',
            ['í'] = 'i',
            ['ì'] = 'i',
            ['î'] = 'i',
            ['ó'] = 'o',
            ['ò'] = 'o',
            ['õ'] = 'o',
            ['ô'] = 'o',
            ['ù'] = 'u',
            ['ú'] = 'u',
            ['û'] = 'u',
            ['-'] = ' ',
            ['–'] = ' '
        };

        private string TratarString(string entrada)
        {
            entrada = RemoverStopWords(entrada);
            entrada = RemoverAcentos(entrada);
            return entrada;
        }

        private string RemoverStopWords(string entrada)
        {
            foreach (var stopword in Stopwords)
            {
                entrada = entrada.Replace(stopword, " ");
            }
            return entrada;
        }

        private string RemoverAcentos(string entrada)
        {
            foreach (var item in Acentuacao)
            {
                entrada = entrada.Replace(item.Key, item.Value);
            }
            return entrada;
        }

        private IEnumerable<ProdutoProcessadoCategorizado> ProcessarProdutos(IEnumerable<ProdutoCategorizadoManualmente> produtosNaoProcessados)
        {
            var retorno = new List<ProdutoProcessadoCategorizado>();
            foreach (var produto in produtosNaoProcessados)
            {
                var produtoProcessado = new ProdutoProcessadoCategorizado();
                var nome = TratarString(produto.Nome.ToLower()).Split(' ').Where(a => a.Length > 0).ToList();
                var marca = string.Empty;
                if (!string.IsNullOrEmpty(produto.Marca))
                    marca = TratarString(produto.Marca.ToLower());

                var categoria = new List<string>();
                if (!string.IsNullOrEmpty(produto.CategoriaParceiro))
                    categoria = TratarString(produto.CategoriaParceiro.ToLower()).Split(' ').Where(a => a.Length > 0).ToList();

                var subcategoria = new List<string>();
                if (!string.IsNullOrEmpty(produto.SubcategoriaParceiro))
                    subcategoria = TratarString(produto.SubcategoriaParceiro.ToLower()).Split(' ').Where(a => a.Length > 0).ToList();

                produtoProcessado.Palavras.AddRange(nome);
                if (!string.IsNullOrEmpty(marca))
                    produtoProcessado.Palavras.Add(marca);
                produtoProcessado.Palavras.AddRange(categoria);
                produtoProcessado.Palavras.AddRange(subcategoria);
                produtoProcessado.IdSubcategoria = produto.IdSubcategoria;
                retorno.Add(produtoProcessado);
            }
            return retorno;
        }

        public ProdutoProcessado ProcessarProduto(Produto produtosNaoProcessados)
        {
            var produtoProcessado = new ProdutoProcessado();

            var nome = TratarString(produtosNaoProcessados.Nome.ToLower()).Split(' ').Where(a => a.Length > 0).ToList();
            var marca = string.Empty;
            if (!string.IsNullOrEmpty(produtosNaoProcessados.Marca))
                marca = TratarString(produtosNaoProcessados.Marca.ToLower());

            var categoria = new List<string>();
            if (!string.IsNullOrEmpty(produtosNaoProcessados.CategoriaParceiro))
                categoria = TratarString(produtosNaoProcessados.CategoriaParceiro.ToLower()).Split(' ').Where(a => a.Length > 0).ToList();

            var subcategoria = new List<string>();
            if (!string.IsNullOrEmpty(produtosNaoProcessados.SubcategoriaParceiro))
                subcategoria = TratarString(produtosNaoProcessados.SubcategoriaParceiro.ToLower()).Split(' ').Where(a => a.Length > 0).ToList();

            produtoProcessado.Palavras.AddRange(nome);
            if (!string.IsNullOrEmpty(marca))
                produtoProcessado.Palavras.Add(marca);
            produtoProcessado.Palavras.AddRange(categoria);
            produtoProcessado.Palavras.AddRange(subcategoria);
            return produtoProcessado;
        }

        public IEnumerable<Subcategoria> ProcessarESepararProdutosCategorizados(IEnumerable<ProdutoCategorizadoManualmente> baseDeConhecimento, IEnumerable<Categoria> categoriasUtilizadas)
        {
            var produtosProcessadosCategorizados = ProcessarProdutos(baseDeConhecimento);
            var subcategorias = categoriasUtilizadas.SelectMany(a => a.Subcategorias).Select(a => new Subcategoria() { IdNumerico = a.IdNumerico, Nome = a.Nome });
            var retorno = new List<Subcategoria>();
            foreach (var item in subcategorias)
            {
                var produtosDaCategoria = produtosProcessadosCategorizados.Where(p => p.IdSubcategoria == item.IdNumerico);
                var produtosProcessados = produtosDaCategoria
                    .Select(a => new ProdutoProcessado { Palavras = a.Palavras })
                    .ToList();
                item.Produtos = produtosProcessados;
                retorno.Add(item);
            }
            return retorno;
        }
    }
}
