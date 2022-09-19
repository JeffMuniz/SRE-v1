using AutoMapper;
using SharedUsecases = Shared.Backend.Application.Usecases;

namespace Product.Categorization.Worker.Backend.Application.Usecases.Shared.Mappings
{
    public class ErrorMap : Profile
    {
        public ErrorMap()
        {
            CreateMap<SharedUsecases.Models.Error, Domain.ValueObjects.ErrorType>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(source => source.Code)
                )
                .ForMember(
                    dest => dest.Error,
                    opt => opt.MapFrom(source => source.Message)
                )
                .ReverseMap();
        }
    }
}
