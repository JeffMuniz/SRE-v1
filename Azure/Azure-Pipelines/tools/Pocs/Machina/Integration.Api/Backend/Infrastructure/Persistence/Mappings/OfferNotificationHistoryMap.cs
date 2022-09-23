using AutoMapper;
using Integration.Api.Backend.Infrastructure.Persistence.Entities;

namespace Integration.Api.Backend.Infrastructure.Persistence.Mappings
{
    public class OfferNotificationHistoryMap : Profile
    {
        public OfferNotificationHistoryMap()
        {
            CreateMap<Domain.Entities.OfferNotificationHistory, OfferNotificationHistory>()
                .ForMember(
                    dest => dest.OfferNotification,
                    opt => opt.MapFrom(source => source.Changes)
                )
                .ReverseMap();
        }
    }
}
