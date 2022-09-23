using SharedUsecaseModels = Shared.Backend.Application.Usecases.Models;
using SharedUsecasePersistenceModels = Product.Persistence.Worker.Backend.Application.Usecases.Shared.Models;

namespace Product.Persistence.Worker.Backend.Application.Usecases.UpsertSku.Models
{
    public class Inbound
    {
        public SharedUsecaseModels.SupplierSku SupplierSku { get; set; }

        public SharedUsecasePersistenceModels.CategorizationData CategorizationData { get; set; }

        public SharedUsecasePersistenceModels.EnrichedData EnrichedData { get; set; }
    }
}
