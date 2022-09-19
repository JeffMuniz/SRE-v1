using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using System.Linq;

namespace System.Linq.Tests
{
    public class ParallelAsyncEnumerableExtensionsTests
    {
        private const int QUANTITY_ITEMS = 1000;
        private readonly TimeSpan ITEM_WAIT_TIME = TimeSpan.FromMilliseconds(1);

        [Theory]
        [InlineData(1)]
        [InlineData(4)]
        [InlineData(8)]
        public async Task Execute_AllItens_In_Parallel_When_IAsyncEnumerable_Source(
            int degreeOfParallelism
        )
        {
            var itemsTotal = new ConcurrentBag<int>();

            var asyncEnumerableItems = GetAsyncEnumerable(QUANTITY_ITEMS, CancellationToken.None);
            await asyncEnumerableItems
                .ParallelForEachAsync(
                    maxDegreeOfParallelism: degreeOfParallelism,
                    async item =>
                    {
                        Trace.WriteLine($"{DateTime.Now.TimeOfDay} -> {item}");
                        await Task.Delay(ITEM_WAIT_TIME);
                        itemsTotal.Add(item);                        
                    })
                    .ToListAsync();

            Assert.True(
                itemsTotal.Count == QUANTITY_ITEMS,
                $"Items: {itemsTotal.Count} of {QUANTITY_ITEMS}");
        }

        [Theory]
        [InlineData(1)]
        [InlineData(4)]
        [InlineData(8)]
        public async Task Execute_AllItens_In_Parallel_When_IAsyncEnumerable_Source_With_Index(
            int degreeOfParallelism
        )
        {
            var itemsTotal = new ConcurrentBag<int>();

            var asyncEnumerableItems = GetAsyncEnumerable(QUANTITY_ITEMS, CancellationToken.None);
            await asyncEnumerableItems
                .ParallelForEachAsync(
                    maxDegreeOfParallelism: degreeOfParallelism,
                    async (index, item) =>
                    {
                        Trace.WriteLine($"{DateTime.Now.TimeOfDay} -> {index}:{item}");
                        await Task.Delay(ITEM_WAIT_TIME);
                        itemsTotal.Add(item);
                    })
                .ToListAsync();

            Assert.True(
                itemsTotal.Count == QUANTITY_ITEMS,
                $"Items: {itemsTotal.Count} of {QUANTITY_ITEMS}");
        }

        private async IAsyncEnumerable<int> GetAsyncEnumerable(int itemsCount, [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            foreach (var item in Enumerable.Range(1, itemsCount))
            {
                cancellationToken.ThrowIfCancellationRequested();

                yield return await Task.FromResult(item);
            }
        }
    }
}
