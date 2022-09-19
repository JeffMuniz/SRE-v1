using AutoMapper;

namespace Availability.Manager.Worker.Backend.Domain.Mappings
{
    public class CacheRenewScheduleMap : Profile
    {
        public CacheRenewScheduleMap()
        {
            CreateMap<Entities.SkuAvailability, ValueObjects.CacheRenewScheduleId>()
                .IncludeMembers(source => source.Id)
                .ReverseMap();

            CreateMap<ValueObjects.SkuAvailabilityId, ValueObjects.CacheRenewScheduleId>()
                .ReverseMap();

            CreateMap<ValueObjects.ShardId, ValueObjects.CacheRenewScheduleId>()
                .ForMember(
                    dest => dest.ShardId,
                    opt => opt.MapFrom(source => source)
                );
        }
    }
}
