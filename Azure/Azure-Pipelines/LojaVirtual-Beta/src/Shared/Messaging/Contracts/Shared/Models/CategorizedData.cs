using System.Collections.Generic;

namespace Shared.Messaging.Contracts.Shared.Models
{
    public class CategorizedData
    {
        public int? SubcategoryId { get; set; }

        public double ApprovalThreshold { get; set; }

        public IEnumerable<CategorizedDataProbability> TopSubcategoriesProbabilities { get; set; }
    }
}
