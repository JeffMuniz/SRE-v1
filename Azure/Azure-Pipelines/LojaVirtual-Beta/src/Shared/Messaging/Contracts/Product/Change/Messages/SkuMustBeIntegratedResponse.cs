using System;

namespace Shared.Messaging.Contracts.Product.Change.Messages
{
    public interface SkuMustBeIntegratedResponse
    {
        bool MustBeIntegrated { get; set; }
    }
}
