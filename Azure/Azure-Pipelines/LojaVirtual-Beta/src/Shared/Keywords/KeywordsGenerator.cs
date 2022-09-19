using Microsoft.ML;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shared.Keywords
{
    public class KeywordsGenerator : IKeywordsGenerator
    {
        private readonly MLContext _context;

        public KeywordsGenerator()
        {
            _context = new MLContext();
        }

        public async Task<IEnumerable<string>> Generate(string text)
        {
            var emptyData = new List<TextData>();

            var data = _context.Data.LoadFromEnumerable(emptyData);

            var tokenization = _context.Transforms.Text.TokenizeIntoWords("Tokens", "Text", separators: new[] { ' ', '.', ',' })
                .Append(_context.Transforms.Text.RemoveDefaultStopWords("Tokens", "Tokens",
                    Microsoft.ML.Transforms.Text.StopWordsRemovingEstimator.Language.Portuguese_Brazilian))
                .Append(_context.Transforms.Text.RemoveStopWords("Tokens", "Tokens", new[] { "da", "de", "do", " - " }));

            using var stopWordsModel = tokenization.Fit(data);

            using var engine = _context.Model.CreatePredictionEngine<TextData, TextTokens>(stopWordsModel);

            var newText = engine.Predict(new TextData { Text = text });

            var keywords = newText?.Tokens?.Distinct()
                ?? Enumerable.Empty<string>();

            return await Task.FromResult(keywords);
        }
    }
}
