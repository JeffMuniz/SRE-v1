using System.Collections.Generic;

namespace Shared.Backend.Application.Usecases.Models
{
    public class Image
    {
        public int Order { get; set; }

        public IEnumerable<ImageSize> Sizes { get; set; }
    }
}
