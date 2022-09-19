namespace Product.Categorization.Worker.Backend.Application.Usecases.CategorizeSku.Options
{
    public class CategorizeOptions
    {
        public double ApprovalThreshold { get; set; }

        public int TopProbabilities { get; set; }
    }
}
