using AutoMapper;
using SharedUsecases = Shared.Backend.Application.Usecases;

namespace Search.Worker.Backend.Application.Usecases.Shared.Mappings
{
    public class ErrorMap : Profile
    {
        public ErrorMap()
        {
            CreateMap<Domain.ValueObjects.ErrorType, SharedUsecases.Models.Error>()
                .ForMember(
                    dest => dest.Code,
                    opt => opt.MapFrom(source => source.Id)
                )
                .ForMember(
                    dest => dest.Message,
                    opt => opt.MapFrom(source => source.Error)
                );
        }
    }
}
