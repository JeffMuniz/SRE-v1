using CSharpFunctionalExtensions;
using Humanizer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Search.Worker.Backend.Domain.ValueObjects
{
    public class FeatureType : EnumValueObject<FeatureType>
    {
        public static readonly FeatureType Color = new("1", "Cor", new[] { "Color" });

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
