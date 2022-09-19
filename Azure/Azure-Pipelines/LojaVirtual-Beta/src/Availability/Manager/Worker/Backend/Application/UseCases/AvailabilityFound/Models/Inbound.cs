using Shared.Backend.Application.Usecases.Models;
using System;

namespace Availability.Manager.Worker.Backend.Application.UseCases.AvailabilityFound.Models
{
    public class Inbound
    {
        public int SupplierId { get; set; }

        public string SupplierSkuId { get; set; }

        public string ContractId { get; set; }

        public string PersistedSkuId { get; set; }

        public bool MainContract { get; set; }

        public bool Available { get; set; }

        public Price Price { get; set; }

        public string ShardId { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
