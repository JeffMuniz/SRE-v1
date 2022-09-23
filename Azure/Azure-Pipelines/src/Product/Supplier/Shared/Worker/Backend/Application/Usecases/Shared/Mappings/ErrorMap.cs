using AutoMapper;
using SharedUsecases = Shared.Backend.Application.Usecases;

namespace Product.Supplier.Shared.Worker.Backend.Application.Usecases.Shared.Mappings
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

            CreateMap<System.Exception, SharedUsecases.Models.Error>()
                .ForMember(
                    dest => dest.Code,
                    opt => opt.MapFrom(source => source.GetType().Name)
                )
                .ForMember(
                    dest => dest.Message,
                    opt => opt.MapFrom(source => source.Message)
                )
                .ReverseMap();
        }
    }
}
