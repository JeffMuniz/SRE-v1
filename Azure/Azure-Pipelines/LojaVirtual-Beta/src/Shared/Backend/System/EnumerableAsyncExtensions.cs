using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace System.Linq
{
    public static class EnumerableAsyncExtensions
    {
        public static Task<IEnumerable<T>> ForEachAsync<T>(this IEnumerable<T> collection, Func<T, Task> action, CancellationToken cancellation = default)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));
            if (action == null) throw new ArgumentNullException(nameof(action));

            return collection.ForEachInternalAsync(action, cancellation);
        }

        public static Task<IEnumerable<T>> ForEachAsync<T>(this IEnumerable<T> collection, Func<long, T, Task> action, CancellationToken cancellation = default)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));
            if (action == null) throw new ArgumentNullException(nameof(action));

            return collection.ForEachInternalAsync(action, cancellation);
        }

        private static async Task<IEnumerable<T>> ForEachInternalAsync<T>(this IEnumerable<T> collection, Func<T, Task> action, CancellationToken cancellation = default)
        {
            foreach (var item in collection)
            {
                if (cancellation.IsCancellationRequested)
                    return collection;

                await action(item);
            }

            return collection;
        }

        private static async Task<IEnumerable<T>> ForEachInternalAsync<T>(this IEnumerable<T> collection, Func<long, T, Task> action, CancellationToken cancellation = default)
        {
            var index = 0;
            foreach (var item in collection)
            {
                if (cancellation.IsCancellationRequested)
                    return collection;

                await action(index++, item);
            }

            return collection;
        }

        public static Task<IEnumerable<T>> ParallelForEachAsync<T>(this IEnumerable<T> collection, Func<T, Task> action, CancellationToken cancellation = default)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));
            if (action == null) throw new ArgumentNullException(nameof(action));

            return collection.ParallelForEachAsync(Environment.ProcessorCount, action, cancellation);
        }

        public static Task<IEnumerable<T>> ParallelForEachAsync<T>(this IEnumerable<T> collection, int maxDegreeOfParallelism, Func<T, Task> action, CancellationToken cancellation = default)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));
            if (action == null) throw new ArgumentNullException(nameof(action));
            if (maxDegreeOfParallelism < 1) throw new ArgumentOutOfRangeException(nameof(maxDegreeOfParallelism));

            return collection.ParallelForEachInternalAsync(maxDegreeOfParallelism, action, cancellation);
        }

        public static Task<IEnumerable<T>> ParallelForEachAsync<T>(this IEnumerable<T> collection, Func<long, T, Task> action, CancellationToken cancellation = default)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));
            if (action == null) throw new ArgumentNullException(nameof(action));

            return collection.ParallelForEachInternalAsync(Environment.ProcessorCount, action, cancellation);
        }

        public static Task<IEnumerable<T>> ParallelForEachAsync<T>(this IEnumerable<T> collection, int maxDegreeOfParallelism, Func<long, T, Task> action, CancellationToken cancellation = default)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));
            if (action == null) throw new ArgumentNullException(nameof(action));
            if (maxDegreeOfParallelism < 1) throw new ArgumentOutOfRangeException(nameof(maxDegreeOfParallelism));

            return collection.ParallelForEachInternalAsync(maxDegreeOfParallelism, action, cancellation);
        }

        private static async Task<IEnumerable<T>> ParallelForEachInternalAsync<T>(this IEnumerable<T> collection, int maxDegreeOfParallelism, Func<T, Task> action, CancellationToken cancellation = default)
        {
            var block = new ActionBlock<T>(
                async item => await action(item),
                new ExecutionDataflowBlockOptions
                {
                    MaxDegreeOfParallelism = maxDegreeOfParallelism,
                    CancellationToken = cancellation
                }
            );

            using (var collectionEnumerator = collection.GetEnumerator())
            {
                try
                {
                    collectionEnumerator.Reset();
                }
                catch (NotSupportedException)
                {
                    // Continues when the enumerator does not implement Reset
                }

                while (collectionEnumerator.MoveNext())
                {
                    if (cancellation.IsCancellationRequested)
                        return collection;

                    var currentEnumeratorItem = collectionEnumerator.Current;
                    await block.SendAsync(currentEnumeratorItem, cancellation);
                }

                block.Complete();
                await block.Completion;
            }

            return collection;
        }

        private static async Task<IEnumerable<T>> ParallelForEachInternalAsync<T>(this IEnumerable<T> collection, int maxDegreeOfParallelism, Func<long, T, Task> action, CancellationToken cancellation = default)
        {
            var block = new ActionBlock<KeyValuePair<long, T>>(
                async item => await action(item.Key, item.Value),
                new ExecutionDataflowBlockOptions
                {
                    MaxDegreeOfParallelism = maxDegreeOfParallelism,
                    CancellationToken = cancellation
                }
            );

            using (var collectionEnumerator = collection.GetEnumerator())
            {
                try
                {
                    collectionEnumerator.Reset();
                }
                catch (NotSupportedException)
                {
                    // Continues when the enumerator does not implement Reset
                }

                var currentIndex = 0L;
                while (collectionEnumerator.MoveNext())
                {
                    if (cancellation.IsCancellationRequested)
                        return collection;

                    var currentEnumeratorItem = collectionEnumerator.Current;
                    var indexEnumeratorItem = currentIndex;

                    await block.SendAsync(KeyValuePair.Create(indexEnumeratorItem, currentEnumeratorItem), cancellation);

                    currentIndex++;
                }

                block.Complete();

                await block.Completion;
            }

            return collection;
        }
    }
}
