using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace System.Linq
{
    public static class ParallelAsyncEnumerableExtensions
    {
        public static IAsyncEnumerable<T> ParallelForEachAsync<T>(this IAsyncEnumerable<T> collection, Func<T, Task> action, CancellationToken cancellation = default)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));
            if (action == null) throw new ArgumentNullException(nameof(action));

            return collection.ParallelForEachAsync(Environment.ProcessorCount, action, cancellation);
        }

        public static IAsyncEnumerable<T> ParallelForEachAsync<T>(this IAsyncEnumerable<T> collection, int maxDegreeOfParallelism, Func<T, Task> action, CancellationToken cancellation = default)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));
            if (action == null) throw new ArgumentNullException(nameof(action));
            if (maxDegreeOfParallelism < 1) throw new ArgumentOutOfRangeException(nameof(maxDegreeOfParallelism));

            return collection.ParallelForEachInternalAsync(maxDegreeOfParallelism, action, cancellation);
        }

        public static IAsyncEnumerable<T> ParallelForEachAsync<T>(this IAsyncEnumerable<T> collection, Func<long, T, Task> action, CancellationToken cancellation = default)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));
            if (action == null) throw new ArgumentNullException(nameof(action));

            return collection.ParallelForEachInternalAsync(Environment.ProcessorCount, action, cancellation);
        }

        public static IAsyncEnumerable<T> ParallelForEachAsync<T>(this IAsyncEnumerable<T> collection, int maxDegreeOfParallelism, Func<long, T, Task> action, CancellationToken cancellation = default)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));
            if (action == null) throw new ArgumentNullException(nameof(action));
            if (maxDegreeOfParallelism < 1) throw new ArgumentOutOfRangeException(nameof(maxDegreeOfParallelism));

            return collection.ParallelForEachInternalAsync(maxDegreeOfParallelism, action, cancellation);
        }

        private static async IAsyncEnumerable<T> ParallelForEachInternalAsync<T>(this IAsyncEnumerable<T> collection, int maxDegreeOfParallelism, Func<T, Task> action, [EnumeratorCancellation] CancellationToken cancellation = default)
        {
            var options = new ExecutionDataflowBlockOptions
            {
                SingleProducerConstrained = true,
                MaxDegreeOfParallelism = maxDegreeOfParallelism,
                CancellationToken = cancellation
            };
            var block = new ActionBlock<T>(
                item => action(item),
                options
            );

            await using (var collectionEnumerator = collection.GetAsyncEnumerator(cancellation))
            {
                while (await collectionEnumerator.MoveNextAsync())
                {
                    var currentEnumeratorItem = collectionEnumerator.Current;
                    await block.SendAsync(currentEnumeratorItem, cancellation);

                    yield return currentEnumeratorItem;
                }

                block.Complete();
                await block.Completion;
            }
        }

        private static async IAsyncEnumerable<T> ParallelForEachInternalAsync<T>(this IAsyncEnumerable<T> collection, int maxDegreeOfParallelism, Func<long, T, Task> action, [EnumeratorCancellation] CancellationToken cancellation = default)
        {
            var options = new ExecutionDataflowBlockOptions
            {
                SingleProducerConstrained = true,
                MaxDegreeOfParallelism = maxDegreeOfParallelism,
                CancellationToken = cancellation
            };
            var block = new ActionBlock<KeyValuePair<long, T>>(
                item => action(item.Key, item.Value),
                options
            );

            await using (var collectionEnumerator = collection.GetAsyncEnumerator(cancellation))
            {
                var currentIndex = 0L;
                while (await collectionEnumerator.MoveNextAsync())
                {
                    var currentEnumeratorItem = collectionEnumerator.Current;
                    var indexEnumeratorItem = currentIndex;

                    await block.SendAsync(KeyValuePair.Create(indexEnumeratorItem, currentEnumeratorItem), cancellation);

                    currentIndex++;

                    yield return currentEnumeratorItem;
                }

                block.Complete();
                await block.Completion;
            }
        }
    }
}
