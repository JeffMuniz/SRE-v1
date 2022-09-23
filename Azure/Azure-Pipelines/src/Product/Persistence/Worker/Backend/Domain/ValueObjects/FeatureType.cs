using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Product.Persistence.Worker.Backend.Domain.ValueObjects
{
    public class FeatureType : EnumValueObject<FeatureType>
    {
        public static readonly FeatureType Color = new("1", "Cor", new[] { "Color" });

        public static readonly FeatureType Size = new("2", "Tamanho", new[] { "Size" });

        public static readonly FeatureType Voltage = new("3", "Voltagem", new[] { "Voltage", "Tensão", "Alimentação" });

        public static readonly FeatureType Model = new("4", "Modelo", new[] { "Model" });

        public static readonly FeatureType GeneralFeature = new("5", "Características Gerais");

        public static readonly FeatureType TechnicalSpecification = new("6", "Especificações Técnicas");

        public string Name { get; }

        public IEnumerable<string> Synonyms { get; } = new List<string>();

        public FeatureType(string id, string name) : base(id)
        {
            Name = name;
        }

        public FeatureType(string id, string name, IEnumerable<string> synonyms) : this(id, name)
        {
            Synonyms = Synonyms
                .Append(Name.NormalizeCompare())
                .Concat(synonyms.DefaultIfNull().Select(x => x.NormalizeCompare()));
        }

        public override string ToString() =>
            $"{Id}|{Name}";

        public int ToInteger() =>
            int.Parse(Id);
    }
}
