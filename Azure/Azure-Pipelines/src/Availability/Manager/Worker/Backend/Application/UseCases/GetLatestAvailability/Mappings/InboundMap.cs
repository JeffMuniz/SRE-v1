using AutoMapper;

namespace Availability.Manager.Worker.Backend.Application.UseCases.GetLatestAvailability.Mappings
{
    public class InboundMap : Profile
    {
        public InboundMap()
        {
            CreateMap<Models.Inbound, Domain.Entities.SkuAvailability>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(source => source)
                )
                .ReverseMap();

            CreateMap<Models.Inbound, Domain.ValueObjects.SkuAvailabilityId>()
                .ReverseMap();

            CreateMap<Models.Inbound, Domain.ValueObjects.ShardId>()
                .ConstructUsing(source =>
                    string.IsNullOrWhiteSpace(source.ShardId)
                        ? null
                        : new Domain.ValueObjects.ShardId(source.ShardId)
                );
        }
    }
}
