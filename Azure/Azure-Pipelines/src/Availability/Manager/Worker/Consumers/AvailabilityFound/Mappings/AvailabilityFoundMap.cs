using AutoMapper;
using MassTransit;
using AvailabilityMessaging = Shared.Messaging.Contracts.Availability.Messages;
using UsecaseModels = Availability.Manager.Worker.Backend.Application.UseCases.AvailabilityFound.Models;

namespace Availability.Manager.Worker.Consumers.AvailabilityFound.Mappings
{
    public class AvailabilityFoundMap : Profile
    {
        public AvailabilityFoundMap()
        {
            CreateMap<ConsumeContext<AvailabilityMessaging.Search.AvailabilityFound>, UsecaseModels.Inbound>()
                .ForMember(
                    dest => dest.CreatedDate,
                    opt =>
                    {
                        opt.PreCondition(source => source.SentTime.HasValue);
                        opt.MapFrom(source => source.SentTime);
                    }
                );

            CreateMap<AvailabilityMessaging.Search.AvailabilityFound, UsecaseModels.Inbound>()
                 .ReverseMap();
        }
    }
}
