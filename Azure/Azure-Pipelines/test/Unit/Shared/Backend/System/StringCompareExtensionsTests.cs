using Xunit;

namespace System.Tests
{
    public class StringCompareExtensionsTests
    {
        [Theory]
        [InlineData("APRESENTAÇÃO DO PRODUTO")]
        [InlineData("APRESENTACÃO DO PRODUTO")]
        [InlineData("APRESENTAÇAO DO PRODUTO")]
        [InlineData("APRESENTACAO DO PRODUTO")]
        [InlineData("Apresentação do produto")]
        [InlineData("Apresentacão do produto")]
        [InlineData("Apresentaçao do produto")]
        [InlineData("Apresentacao do produto")]
        public void NormalizeCompareWithDiacritic(string text)
        {
            const string ExpectedText = "ApresentacaoDoProduto";

            var normalizedText = text.NormalizeCompare();

            Assert.Equal(ExpectedText, normalizedText);
        }

        [Theory]
        [InlineData("APRESENTAÇÃO DO PRODUTO", "ApresentaçãoDoProduto")]
        [InlineData("APRESENTACÃO DO PRODUTO", "ApresentacãoDoProduto")]
        [InlineData("APRESENTAÇAO DO PRODUTO", "ApresentaçaoDoProduto")]
        [InlineData("APRESENTACAO DO PRODUTO", "ApresentacaoDoProduto")]
        [InlineData("Apresentação do produto", "ApresentaçãoDoProduto")]
        [InlineData("Apresentacão do produto", "ApresentacãoDoProduto")]
        [InlineData("Apresentaçao do produto", "ApresentaçaoDoProduto")]
        [InlineData("Apresentacao do produto", "ApresentacaoDoProduto")]
        public void NormalizeCompareWithoutDiacritic(string text, string expectedText)
        {
            var normalizedText = text.NormalizeCompare(normalizeDiacritics: false);

            Assert.Equal(expectedText, normalizedText);
        }
    }
}
