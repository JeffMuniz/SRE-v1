using AutoMapper;
using Availability.Manager.Worker.Backend.Infrastructure.Cache.Models;

namespace Availability.Manager.Worker.Backend.Infrastructure.Cache.Mappings
{
    public class CashAvailabilityMap : Profile
    {
        public CashAvailabilityMap()
        {
            CreateMap<Domain.Entities.SkuAvailability, CashAvailability>()
                .ForMember(
                    dest => dest.SkuId,
                    opt => opt.MapFrom(source => source.PersistedSkuId)
                )
                .IncludeMembers(source => source.Id)
                .ReverseMap();

            CreateMap<Domain.ValueObjects.SkuAvailabilityId, CashAvailability>()
                .ForMember(
                    dest => dest.SupplierContractId,
                    opt => opt.MapFrom(source => source.ContractId)
                )
                .ReverseMap();
        }
    }
}
