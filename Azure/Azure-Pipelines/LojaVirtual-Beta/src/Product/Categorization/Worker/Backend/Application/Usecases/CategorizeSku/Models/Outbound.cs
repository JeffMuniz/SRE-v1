using System.Collections.Generic;

namespace Product.Categorization.Worker.Backend.Application.Usecases.CategorizeSku.Models
{
    public class Outbound
    {
        public int? SubcategoryId { get; set; }

        public double ApprovalThreshold { get; set; }

        public IEnumerable<SubcategoryProbability> TopSubcategoriesProbabilities { get; set; }
    }
}
