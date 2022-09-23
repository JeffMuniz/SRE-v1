using System.Collections.Generic;
using System.Linq;

namespace Availability.Search.Worker.Backend.Infrastructure.ExternalServices.Configuration.Models
{
    public class Partner
    {
        public string PartnerCode { get; set; }

        public int SupplierId { get; set; }

        public IEnumerable<Contract> Contracts { get; set; }

        public static IEnumerable<Partner> FromPartnerContracts(
            IEnumerable<IGrouping<(string Connector, string PartnerCode), string>> partnerContracts,
            AllPartnersResult partners
        ) =>
             partnerContracts
                .Join(
                    partners,
                    contract => (contract.Key.Connector, contract.Key.PartnerCode),
                    partner => (partner.Connector, partner.Partner),
                    (contract, partner) =>
                    {
                        return new Partner
                        {
                            PartnerCode = contract.Key.PartnerCode,
                            SupplierId = partner.CommonParameters.SupplierId,
                            Contracts = contract.Select(x => new Contract
                            {
                                ContractId = x
                            })
                        };
                    }
                );
    }
}
