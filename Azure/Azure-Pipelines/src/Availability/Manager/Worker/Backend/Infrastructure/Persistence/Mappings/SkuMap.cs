using AutoMapper;
using System.Collections.Generic;
using System.Linq;

namespace Availability.Manager.Worker.Backend.Infrastructure.Persistence.Mappings
{
    public class SkuMap : Profile
    {
        public SkuMap()
        {            
            CreateMap<Domain.Entities.SkuAvailability, Entities.Sku>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(source => source.Id)
                )
                .ReverseMap();

            CreateMap<Domain.ValueObjects.SkuAvailabilityId, Entities.SkuId>()
                .ReverseMap();

            CreateMap<IEnumerable<Entities.Sku>, Domain.ValueObjects.PagedSkuAvailability>()
                .ForMember(
                    dest => dest.Skus,
                    opt => opt.MapFrom(source => source)
                )
                .ForMember(
                    dest => dest.Total,
                    opt => opt.Ignore()
                );

            CreateMap<long, Domain.ValueObjects.PagedSkuAvailability>()
                .ForMember(
                    dest => dest.Total,
                    opt => opt.MapFrom(source => source)
                );
        }
    }
}
