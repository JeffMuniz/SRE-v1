namespace Shared.Backend.Application.Usecases.Models
{
    public class Subcategory
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public Category Category { get; set; }
    }
}
