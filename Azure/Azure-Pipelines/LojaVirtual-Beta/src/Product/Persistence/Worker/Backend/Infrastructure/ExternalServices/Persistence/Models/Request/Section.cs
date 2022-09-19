namespace Product.Persistence.Worker.Backend.Infrastructure.ExternalServices.Persistence.Models.Request
{
    public class Section
    {
        public int SectionTypeId { get; set; }

        public string SectionId { get; set; }

        public string Value { get; set; }

        public string SectionParentId { get; set; }
    }
}
