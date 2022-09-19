using CSharpFunctionalExtensions;

namespace Shared.Backend.Domain.ValueObjects
{
    public class ImageSizeType : EnumValueObject<ImageSizeType>
    {
        public static readonly ImageSizeType Small = new(nameof(Small), 150, 150);

        public static readonly ImageSizeType Medium = new(nameof(Medium), 350, 350);

        public static readonly ImageSizeType Large = new(nameof(Large), 1000, 1000);

        public int Width { get; init; }

        public int Height { get; init; }

        public ImageSizeType(string id, int width, int height) : base(id)
        {
            Width = width;
            Height = height;
        }

        public override string ToString() =>
            $"{Id}";
    }
}
