using AutoMapper;
using MessagingContracts = Shared.Messaging.Contracts;
using SharedUsecases = Shared.Backend.Application.Usecases;

namespace Availability.Manager.Worker.Consumers.Shared
{
    public class ErrorMap : Profile
    {
        public ErrorMap()
        {
            CreateMap<MessagingContracts.Shared.Messages.NotFound, SharedUsecases.Models.Error>()
                .ForMember(
                    dest => dest.Code,
                    opt => opt.MapFrom(source => "NotFound")
                )
                .ReverseMap();

            CreateMap<MessagingContracts.Shared.Messages.UnexpectedError, SharedUsecases.Models.Error>()
                .ForMember(
                    dest => dest.Code,
                    opt => opt.MapFrom(source => "Unexpected")
                )
                .ReverseMap();

            CreateMap<MessagingContracts.Shared.Messages.UnexpectedError, System.Exception>()
                .ReverseMap();
        }
    }
}
