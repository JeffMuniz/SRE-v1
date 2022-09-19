using System;
using System.Collections.Generic;
using Xunit;

namespace Shared.Backend.Tests.System
{
    public class StringComparableExtensionsTests
    {
        [Theory]
        [InlineData(StringComparison.CurrentCulture, "TesT3#a&é$@", "TesT3#a&é$@", "TesT4#a&é$@")]
        [InlineData(StringComparison.CurrentCultureIgnoreCase, "TesT3#a&é$@", "Test3#a&é$@", "TesT4#a&é$@")]
        [InlineData(StringComparison.InvariantCulture, "TesT3#a&é$@", "TesT3#a&é$@", "TesT4#a&é$@")]
        [InlineData(StringComparison.InvariantCultureIgnoreCase, "TesT3#a&é$@", "Test3#a&é$@", "TesT4#a&é$@")]
        [InlineData(StringComparison.Ordinal, "TesT3#a&é$@", "TesT3#a&é$@", "TesT4#a&é$@")]
        [InlineData(StringComparison.OrdinalIgnoreCase, "TesT3#a&é$@", "Test3#a&é$@", "TesT4#a&é$@")]
        public void When(
            StringComparison stringComparison,
            string stringX,
            string stringY,
            string stringZ
        )
        {
            Asserts(stringComparison, stringX, stringY, stringZ);
        }

        private static void Asserts(StringComparison stringComparison, string x, string y, string z)
        {
            string nullString = null;
            IEnumerable<string> xEnumerable = new[] { x };
            IEnumerable<string> yEnumerable = new[] { y };
            IEnumerable<string> zEnumerable = new[] { z };
            IEnumerable<string> nullStringEnumerable = new[] { nullString };

            Assert.True(x.Is(stringComparison, y), "Is comparable true");
            Assert.False(x.Is(stringComparison, z), "Is comparable false");
            Assert.False(x.Is(stringComparison, nullString), "Is comparable x with nullString");
            Assert.False(nullString.Is(stringComparison, x), "Is comparable nullString with x");
            Assert.True(nullString.Is(stringComparison, nullString), "Is comparable nullString with nullString");

            Assert.True(x.NotIs(stringComparison, z), "NotIs comparable true");
            Assert.False(x.NotIs(stringComparison, y), "NotIs comparable false");
            Assert.True(x.NotIs(stringComparison, nullString), "NotIs comparable x with nullString");
            Assert.True(nullString.NotIs(stringComparison, x), "NotIs comparable nullString with x");
            Assert.False(nullString.NotIs(stringComparison, nullString), "NotIs comparable nullString with nullString");

            Assert.True(x.In(stringComparison, y), "In comparable true");
            Assert.False(x.In(stringComparison, z), "In comparable false");
            Assert.False(x.In(stringComparison, nullString), "In comparable x with nullString");
            Assert.False(nullString.In(stringComparison, x), "In comparable nullString with x");
            Assert.True(nullString.In(stringComparison, nullString), "In comparable nullString with nullString");

            Assert.True(x.NotIn(stringComparison, z), "NotIn comparable true");
            Assert.False(x.NotIn(stringComparison, y), "NotIn comparable false");
            Assert.True(x.NotIn(stringComparison, nullString), "NotIn comparable x with nullString");
            Assert.True(nullString.NotIn(stringComparison, x), "NotIn comparable nullString with x");
            Assert.False(nullString.NotIn(stringComparison, nullString), "NotIn comparable nullString with nullString");

            Assert.True(x.In(stringComparison, yEnumerable), "In enumerable comparable true");
            Assert.False(x.In(stringComparison, zEnumerable), "In enumerable comparable false");
            Assert.False(x.In(stringComparison, nullStringEnumerable), "In enumerable comparable x with nullString");
            Assert.False(nullString.In(stringComparison, xEnumerable), "In enumerable comparable nullString with x");
            Assert.True(nullString.In(stringComparison, nullStringEnumerable), "In enumerable comparable nullString with nullString");

            Assert.True(x.NotIn(stringComparison, zEnumerable), "NotIn enumerable comparable true");
            Assert.False(x.NotIn(stringComparison, yEnumerable), "NotIn enumerable comparable false");
            Assert.True(x.NotIn(stringComparison, nullStringEnumerable), "NotIn enumerable comparable x with nullString");
            Assert.True(nullString.NotIn(stringComparison, xEnumerable), "NotIn enumerableotIn comparable nullString with x");
            Assert.False(nullString.NotIn(stringComparison, nullStringEnumerable), "NotIn enumerable comparable nullString with nullString");
        }
    }
}
