using AutoMapper;
using ApplicationModels = Availability.Search.Worker.Backend.Application.UseCases.GetAvailabilityForAllContracts.Models;

namespace Availability.Search.Worker.Backend.Application.UseCases.GetAvailabilityForAllContracts.Mappings
{
    public class InboundMap : Profile
    {
        public InboundMap()
        {
            CreateMap<ApplicationModels.Inbound, Domain.ValueObjects.AvailabilityId>()
                .ForMember(
                    dest => dest.ContractId,
                    opt => opt.Ignore()
                )
                .ReverseMap();
        }
    }
}
