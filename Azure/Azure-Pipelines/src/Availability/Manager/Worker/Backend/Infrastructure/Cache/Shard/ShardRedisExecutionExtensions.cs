namespace Availability.Manager.Worker.Backend.Infrastructure.Cache.Shard
{

    public static class ShardRedisExecutionExtensions
    {
        public static IShardRedisExecutionBuilder.IConnectionManagerBuilder BuilderExecution(
            this IShardRedisConnectionManager shardRedisConnectionManager
        ) => ShardRedisExecutionBuilder.Create(shardRedisConnectionManager);
    }
}
