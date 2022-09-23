using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shared.Keywords
{
    public interface IKeywordsGenerator
    {
        Task<IEnumerable<string>> Generate(string text);
    }
}
