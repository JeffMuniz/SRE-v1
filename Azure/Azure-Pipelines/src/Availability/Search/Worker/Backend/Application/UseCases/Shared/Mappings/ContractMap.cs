using AutoMapper;

namespace Availability.Search.Worker.Backend.Application.UseCases.Shared.Mappings
{
    public class ContractMap : Profile
    {
        public ContractMap()
        {
            CreateMap<Domain.ValueObjects.Contract, Domain.ValueObjects.AvailabilityId>()
                .ForMember(
                    dest => dest.ContractId,
                    opt => opt.MapFrom(source => source.Id)
                )
                .ReverseMap();
        }
    }
}
