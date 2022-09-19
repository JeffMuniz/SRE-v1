using Shared.Messaging.Contracts.Availability.Messages.Manager;
using Shared.Messaging.Contracts.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tools.Integration.Models.Availability
{
    public class AvailabilityChangedModel : AvailabilityChanged
    {
        public int SupplierId { get; set; }
        public string SupplierSkuId { get; set; }
        public string ContractId { get; set; }
        public string PersistedSkuId { get; set; }
        public bool MainContract { get; set; }
        public bool Available { get; set; }
        public Price Price { get; set; }
    }
}
