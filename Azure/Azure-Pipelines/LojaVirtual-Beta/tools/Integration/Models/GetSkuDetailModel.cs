using Microsoft.AspNetCore.Mvc;
using ChangeMessages = Shared.Messaging.Contracts.Product.Change.Messages;

namespace Tools.Integration.Models
{
    public class GetSkuDetailModel : ChangeMessages.GetSkuDetail
    {
        [FromRoute(Name = "skuIntegrationId")]
        public string SkuIntegrationId { get; set; }
    }
}
