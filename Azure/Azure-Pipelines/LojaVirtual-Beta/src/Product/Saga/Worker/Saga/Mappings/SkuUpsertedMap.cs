using AutoMapper;
using SagaMessages = Shared.Messaging.Contracts.Product.Saga.Messages;

namespace Product.Saga.Worker.Saga.Mappings
{
    public class SkuUpsertedMap : Profile
    {
        public SkuUpsertedMap()
        {
            CreateMap<SagaMessages.Persistence.SkuUpserted, States.Models.PersistedSku>()
                .ReverseMap();
        }
    }
}

