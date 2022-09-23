using System.Collections.Generic;

namespace Product.Change.Worker.Backend.Infrastructure.Persistence.Integration.Entities
{
    public class Image
    {
        public int Order { get; set; }

        public IEnumerable<ImageSize> Sizes { get; set; }
    }
}
