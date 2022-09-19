using System.Collections.Generic;

namespace Shared.Messaging.Contracts.Shared.Models
{
    public class Image
    {
        public int Order { get; set; }

        public IEnumerable<ImageSize> Sizes { get; set; }
    }
}
