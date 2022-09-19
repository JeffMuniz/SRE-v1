using CSharpFunctionalExtensions;

namespace Availability.Manager.Worker.Backend.Domain.ValueObjects
{
    public class ShardId : EnumValueObject<ShardId>
    {
        public static readonly ShardId AllShards = new(nameof(All));

        public ShardId(string id) : base(id)
        {
        }

        public override string ToString() =>
            $"{Id}";
    }
}
