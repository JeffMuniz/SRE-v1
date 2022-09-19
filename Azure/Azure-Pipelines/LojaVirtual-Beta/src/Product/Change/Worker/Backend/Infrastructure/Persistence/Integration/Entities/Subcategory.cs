namespace Product.Change.Worker.Backend.Infrastructure.Persistence.Integration.Entities
{
    public class Subcategory
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public Category Category { get; set; }
    }
}
