using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using AvailabilityDomain = Availability.Search.Worker.Backend.Domain;

namespace Availability.Search.Worker.Backend.Infrastructure.ExternalServices.Configuration.Mappings
{
    public class ConfigurationMap : Profile
    {
        public ConfigurationMap()
        {
            CreateMap<IEnumerable<Models.Partner>, AvailabilityDomain.ValueObjects.Configuration>()
                .ForMember(
                    dest => dest.Partners,
                    opt => opt.MapFrom(source => source)
                );

            CreateMap<Models.Partner, AvailabilityDomain.ValueObjects.PartnerConfiguration>()
                .ForMember(
                    dest => dest.Contracts,
                    opt => opt.MapFrom(source => source.Contracts)
                );

            CreateMap<Models.Contract, AvailabilityDomain.ValueObjects.Contract>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(source => source.ContractId)
                );

            CreateMap<Shared.Configurations.MainContractConfiguration, AvailabilityDomain.ValueObjects.Contract>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(source => source.MainContract)
                );

            CreateMap<IEnumerable<Shared.Configurations.MainContractConfiguration>, AvailabilityDomain.ValueObjects.Configuration>()
                .ForMember(
                    dest => dest.Partners,
                    opt => opt.MapFrom((source, dest, member, context) =>
                        member.Select(partner => context.Mapper.Map(source, partner)).AsList()
                    )
                );

            CreateMap<IEnumerable<Shared.Configurations.MainContractConfiguration>, AvailabilityDomain.ValueObjects.PartnerConfiguration>()
                .ForMember(
                    dest => dest.MainContract,
                    opt => opt.MapFrom((source, dest) => MapMainContract(dest.SupplierId, source))
                );
        }

        private static AvailabilityDomain.ValueObjects.Contract MapMainContract(
            int supplierId,
            IEnumerable<Shared.Configurations.MainContractConfiguration> mainContractsConfiguration
        ) =>
            mainContractsConfiguration
                .Where(y => y.SupplierId == supplierId)
                .Select(x => new AvailabilityDomain.ValueObjects.Contract
                {
                    Id = x.MainContract
                })
                .FirstOrDefault();
    }
}
