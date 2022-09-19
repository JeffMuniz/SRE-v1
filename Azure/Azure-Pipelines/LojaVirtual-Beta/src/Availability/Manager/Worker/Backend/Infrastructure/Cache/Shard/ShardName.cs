namespace Availability.Manager.Worker.Backend.Infrastructure.Cache.Shard
{
    public record ShardName(string Name)
    {
        public static implicit operator ShardName(string name)
            => new(name);

        public static implicit operator string(ShardName shardName)
            => shardName?.Name;
    }
}
