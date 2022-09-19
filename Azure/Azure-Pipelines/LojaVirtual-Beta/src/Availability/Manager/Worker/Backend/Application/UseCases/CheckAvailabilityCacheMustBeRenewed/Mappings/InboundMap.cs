using AutoMapper;

namespace Availability.Manager.Worker.Backend.Application.UseCases.CheckAvailabilityCacheMustBeRenewed.Mappings
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
                .ForMember(
                    dest => dest.LatestPartnerAvailabilityFoundDate,
                    opt => opt.Ignore()
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
