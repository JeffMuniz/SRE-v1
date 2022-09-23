using CSharpFunctionalExtensions;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Product.Categorization.Worker.Backend.Domain.Services
{
    public interface ICategorizerService
    {
        Task<Result<IEnumerable<ValueObjects.SubcategoryProbability>, ValueObjects.ErrorType>> Compute(
            Entities.Product product,
            CancellationToken cancellationToken
        );
    }
}
