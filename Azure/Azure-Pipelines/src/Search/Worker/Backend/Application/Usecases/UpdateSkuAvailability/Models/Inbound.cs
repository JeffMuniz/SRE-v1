using SharedUsecases = Shared.Backend.Application.Usecases;

namespace Search.Worker.Backend.Application.Usecases.UpdateSkuAvailability.Models
{
    public class Inbound
    {        
        public int SupplierId { get; set; }

        public string SupplierSkuId { get; set; }

        public string ContractId { get; set; }

        public string PersistedSkuId { get; set; }        

        public bool MainContract { get; set; }

        public bool Available { get; set; }

        public SharedUsecases.Models.Price Price { get; set; }
    }
}
