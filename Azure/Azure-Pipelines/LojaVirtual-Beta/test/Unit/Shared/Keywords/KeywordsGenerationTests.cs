using Shared.Keywords;
using System.Threading.Tasks;
using Xunit;

namespace Product.Persistence.Application.Tests
{
    public class KeywordsGenerationTests
    {
        [Fact]
        public async Task Generate()
        {
            var Words = "Esta é uma frase de teste e longa para ver se funciona.";
            var Keywords = new[] { "Esta", "frase", "teste", "longa", "se", "funciona" };

            var keywordsGeneration = new KeywordsGenerator();
            var generatedKeywords = await keywordsGeneration.Generate(Words);

            Assert.Equal(Keywords, generatedKeywords);
        }
    }
}
