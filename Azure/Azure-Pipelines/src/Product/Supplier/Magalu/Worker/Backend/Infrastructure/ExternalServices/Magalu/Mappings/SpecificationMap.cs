using AutoMapper;
using System;
using System.Linq;

namespace Product.Supplier.Magalu.Worker.Backend.Infrastructure.ExternalServices.Magalu.Mappings
{
    public class SpecificationMap : Profile
    {
        public SpecificationMap()
        {
            CreateMap<Models.Specification, Domain.ValueObjects.Specification>()
                .ForMember(
                    dest => dest.Name,
                    opt => opt.MapFrom(source => MapName(source))
                )
                .ForMember(
                    dest => dest.Value,
                    opt => opt.MapFrom(source => MapValue(source))
                );
        }

        private static string MapName(Models.Specification source)
        {
            var name = source.Value
                .Split(":", StringSplitOptions.RemoveEmptyEntries)
                .FirstOrDefault()
                ?.Trim();

            return name;
        }

        private static string MapValue(Models.Specification source)
        {
            var value = string.Join(": ",
                source.Value
                    .Split(":", StringSplitOptions.RemoveEmptyEntries)
                    .Skip(1)
                    .Select(value => value?.Trim())
            )?.Trim();

            return value;
        }
    }
}
