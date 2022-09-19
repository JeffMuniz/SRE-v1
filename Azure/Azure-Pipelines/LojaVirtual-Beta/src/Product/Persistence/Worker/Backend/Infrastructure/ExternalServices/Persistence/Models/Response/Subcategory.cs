namespace Product.Persistence.Worker.Backend.Infrastructure.ExternalServices.Persistence.Models.Response
{
    public class Subcategory
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Category Category { get; set; }
    }
}
