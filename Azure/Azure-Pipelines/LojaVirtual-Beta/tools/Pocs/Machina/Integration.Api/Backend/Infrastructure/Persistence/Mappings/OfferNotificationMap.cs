using AutoMapper;
using Integration.Api.Backend.Infrastructure.Persistence.Entities;

namespace Integration.Api.Backend.Infrastructure.Persistence.Mappings
{
    public class OfferNotificationMap : Profile
    {
        public OfferNotificationMap()
        {
            CreateMap<Domain.Entities.OfferNotification, OfferNotification>()
                .ForMember(
                    dest => dest.Status,
                    opt => opt.MapFrom(source => source.Status.Id)
                )
                .ReverseMap()
                .ForMember(
                    dest => dest.Status,
                    opt => opt.MapFrom(source => new Domain.ValueObjects.NotificationStatus(source.Status))
                );
        }
    }
}
