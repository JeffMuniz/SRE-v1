using AutoMapper;

namespace Product.Change.Worker.Backend.Infrastructure.Messaging.Mappings
{
    public class AddSkuMap : Profile
    {
        public AddSkuMap()
        {
            CreateMap<Domain.Entities.SkuIntegration, Shared.Messaging.Contracts.Product.Saga.Messages.Change.AddSku>()
                .ForMember(
                    dest => dest.SkuIntegrationId,
                    opt => opt.MapFrom(source => source.Id)
                )
                .ReverseMap();
        }
    }
}
