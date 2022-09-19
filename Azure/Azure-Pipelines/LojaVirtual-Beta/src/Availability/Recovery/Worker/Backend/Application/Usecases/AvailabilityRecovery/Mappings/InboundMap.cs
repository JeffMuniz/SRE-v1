using AutoMapper;
using Availability.Recovery.Worker.Backend.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Availability.Recovery.Worker.Backend.Application.Usecases.AvailabilityRecovery.Mappings
{
    public class InboundMap : Profile
    {
        public InboundMap()
        {
            CreateMap<Models.Inbound, SearchFilter>()
                .ReverseMap();
        }
    }
}
