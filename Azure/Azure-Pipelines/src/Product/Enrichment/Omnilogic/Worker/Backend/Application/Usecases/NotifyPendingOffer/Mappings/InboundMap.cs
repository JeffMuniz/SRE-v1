using AutoMapper;

namespace Product.Enrichment.Macnaima.Worker.Backend.Application.Usecases.NotifyPendingOffer.Mappings
{
    public class InboundMap : Profile
    {
        public InboundMap()
        {
            CreateMap<Models.Inbound, Domain.ValueObjects.OfferId>()
                .ForPath(
                    dest => dest.Value,
                    opt => opt.MapFrom(source => source.OfferId)
                );
        }
    }
}
