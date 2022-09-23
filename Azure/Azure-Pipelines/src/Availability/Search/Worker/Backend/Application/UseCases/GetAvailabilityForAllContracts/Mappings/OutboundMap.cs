using AutoMapper;
using System.Collections.Generic;

namespace Availability.Search.Worker.Backend.Application.UseCases.GetAvailabilityForAllContracts.Mappings
{
    public class OutboundMap : Profile
    {
        public OutboundMap()
        {
            CreateMap<IEnumerable<Domain.Entities.Availability>, Models.Outbound>()
                .ConstructUsing((source, context) =>
                    new Models.Outbound(context.Mapper.Map<IEnumerable<Shared.Models.AvailabilityFound>>(source))
                )
                .ReverseMap()
                .ConstructUsing((source, context) =>
                    context.Mapper.Map<IEnumerable<Shared.Models.AvailabilityFound>, IEnumerable<Domain.Entities.Availability>>(source)
                );
        }
    }
}
