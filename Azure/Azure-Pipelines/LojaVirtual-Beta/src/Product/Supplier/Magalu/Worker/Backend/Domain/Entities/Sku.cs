using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using SharedDomain = Shared.Backend.Domain;
using SharedSupplierDomain = Product.Supplier.Shared.Worker.Backend.Domain;

namespace Product.Supplier.Magalu.Worker.Backend.Domain.Entities
{
    public class Sku : Entity<ValueObjects.SkuId>
    {
        private readonly IEnumerable<string> DescriptionSpecificationIdentifiers =
            new[]
            {
                "Apresentação do produto".NormalizeCompare(),
                "Sinopse".NormalizeCompare()
            };

        private readonly int MaxLenghtForFeatureName = 100;

        public string Master { get; init; }

        public string Type { get; init; }

        public string Reference { get; init; }

        public string Description { get; init; }

        public string DescriptionSpecification { get; private set; }

        public SharedSupplierDomain.Entities.Subcategory Subcategory { get; init; }

        public string Brand { get; init; }

        public IEnumerable<ValueObjects.Specification> Specifications { get; private set; } =
            Enumerable.Empty<ValueObjects.Specification>();

        public Color Color { get; private set; }

        public ValueObjects.Voltage Voltage { get; init; }

        public long? TaxClassification { get; init; }

        public int? HasAssembly { get; init; }

        public IEnumerable<SharedDomain.ValueObjects.Image> Images { get; init; }

        public string Video { get; init; }

        public SharedDomain.ValueObjects.Price Price { get; init; }

        public decimal? Freight { get; init; }

        public string Action { get; init; }

        public DateTime? UpdateDate { get; init; }

        public bool? Active { get; init; }

        public Sku AssignColor(IEnumerable<Color> colors)
        {
            if (colors.FirstOrDefault(c => c.Id == Id.Model) is Color color)
                Color = color;

            return this;
        }

        public Sku AssignSpecifications(IEnumerable<ValueObjects.Specification> specifications)
        {
            Specifications = specifications
                .Where(specification =>
                    specification.Name.Length <= MaxLenghtForFeatureName && !DescriptionSpecificationIdentifiers.Contains(specification.Name.NormalizeCompare())
                )
                .AsArray();

            return this;
        }

        public Sku TryAssignDescriptionFromSpecifications(IEnumerable<ValueObjects.Specification> specifications)
        {
            if (specifications.FirstOrDefault(specification =>
                    DescriptionSpecificationIdentifiers.Contains(specification.Name.NormalizeCompare())
                ) is ValueObjects.Specification descriptionSpecification
            )
                DescriptionSpecification = descriptionSpecification.Value.Trim();

            return this;
        }
    }
}
