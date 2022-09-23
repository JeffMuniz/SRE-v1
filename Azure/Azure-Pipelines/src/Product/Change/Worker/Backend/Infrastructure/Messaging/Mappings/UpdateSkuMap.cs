using AutoMapper;

namespace Product.Change.Worker.Backend.Infrastructure.Messaging.Mappings
{
    public class UpdateSkuMap : Profile
    {
        public UpdateSkuMap()
        {
            CreateMap<Domain.Entities.SkuIntegration, Shared.Messaging.Contracts.Product.Saga.Messages.Change.UpdateSku>()
                .ForMember(
                    dest => dest.SkuIntegrationId,
                    opt => opt.MapFrom(source => source.Id)
                )
                .ReverseMap();
        }
    }
}
