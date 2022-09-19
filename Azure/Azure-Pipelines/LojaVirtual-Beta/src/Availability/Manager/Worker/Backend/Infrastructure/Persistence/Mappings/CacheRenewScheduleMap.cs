using AutoMapper;

namespace Availability.Manager.Worker.Backend.Infrastructure.Persistence.Mappings
{
    public class CacheRenewScheduleMap : Profile
    {
        public CacheRenewScheduleMap()
        {
            CreateMap<Domain.Entities.CacheRenewSchedule, Entities.CacheRenewSchedule>()
                .ReverseMap();

            CreateMap<Domain.ValueObjects.CacheRenewScheduleId, Entities.CacheRenewScheduleId>()
                .ForMember(
                    dest => dest.ShardId,
                    opt => opt.MapFrom(source => source.ShardId.Id)
                )
                .ReverseMap()
                .ForMember(
                    dest => dest.ShardId,
                    opt => opt.MapFrom(source => new Domain.ValueObjects.ShardId(source.ShardId))
                );
        }
    }
}
