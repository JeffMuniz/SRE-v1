using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using Xunit;

namespace Shared.Backend.Tests.System
{
    public class ComparableExtensionsTests
    {
        [Fact]
        public void When_String()
        {
            string stringX = "TesT3#a&é$@";
            string stringY = "TesT3#a&é$@";
            string stringZ = "TesT4#a&é$@";

            Asserts(stringX, stringY, stringZ);
        }

        [Fact]
        public void When_Int()
        {
            int intX = 123;
            int intY = 123;
            int intZ = 456;

            Asserts(intX, intY, intZ);
        }

        [Fact]
        public void When_Decimal()
        {
            decimal decimalX = 123.4567m;
            decimal decimalY = 123.4567m;
            decimal decimalZ = 456.0m;

            Asserts(decimalX, decimalY, decimalZ);
        }

        [Fact]
        public void When_Double()
        {
            double doubleX = 123.4567d;
            double doubleY = 123.4567d;
            double doubleZ = 456.0d;

            Asserts(doubleX, doubleY, doubleZ);
        }

        [Fact]
        public void When_Enum()
        {
            StringComparison enumX = StringComparison.Ordinal;
            StringComparison enumY = StringComparison.Ordinal;
            StringComparison enumZ = StringComparison.OrdinalIgnoreCase;

            Asserts(enumX, enumY, enumZ);
        }

        [Fact]
        public void When_DateTime()
        {
            var nowTicks = DateTime.Now.Ticks;
            DateTime dateTimeX = new DateTime(nowTicks);
            DateTime dateTimeY = new DateTime(nowTicks);
            DateTime dateTimeZ = new DateTime(nowTicks).AddMinutes(5);

            Asserts(dateTimeX, dateTimeY, dateTimeZ);
        }

        [Fact]
        public void When_Entity()
        {
            PersonTest personTestX = PersonTest.FromId(1);
            PersonTest personTestY = PersonTest.FromId(1);
            PersonTest personTestZ = PersonTest.FromId(2);

            Asserts(personTestX, personTestY, personTestZ);
        }

        [Fact]
        public void When_ValueObject()
        {
            PersonIdTest idTestX = new() { Id = 1 };
            PersonIdTest idTestY = new() { Id = 1 };
            PersonIdTest idTestZ = new() { Id = 2 };

            Asserts(idTestX, idTestY, idTestZ);
        }

        [Fact]
        public void When_EnumValueObject()
        {
            SizeTest sizeX = SizeTest.Small;
            SizeTest sizeY = SizeTest.Small;
            SizeTest sizeZ = SizeTest.Medium;

            Asserts(sizeX, sizeY, sizeZ);
        }

        private static void Asserts<T>(T x, T y, T z)
        {
            T defaultT = default;
            IEnumerable<T> xEnumerable = new[] { x };
            IEnumerable<T> yEnumerable = new[] { y };
            IEnumerable<T> zEnumerable = new[] { z };
            IEnumerable<T> defaultTEnumerable = new[] { defaultT };

            Assert.True(x.Is(y), "Is comparable true");
            Assert.False(x.Is(z), "Is comparable false");
            Assert.False(x.Is(defaultT), "Is comparable x with defaultT");
            Assert.False(defaultT.Is(x), "Is comparable defaultT with x");
            Assert.True(defaultT.Is(defaultT), "Is comparable defaultT with defaultT");

            Assert.True(x.NotIs(z), "NotIs comparable true");
            Assert.False(x.NotIs(y), "NotIs comparable false");
            Assert.True(x.NotIs(defaultT), "NotIs comparable x with defaultT");
            Assert.True(defaultT.NotIs(x), "NotIs comparable defaultT with x");
            Assert.False(defaultT.NotIs(defaultT), "NotIs comparable defaultT with defaultT");

            Assert.True(x.In(y), "In comparable true");
            Assert.False(x.In(z), "In comparable false");
            Assert.False(x.In(defaultT), "In comparable x with defaultT");
            Assert.False(defaultT.In(x), "In comparable defaultT with x");
            Assert.True(defaultT.In(defaultT), "In comparable defaultT with defaultT");

            Assert.True(x.NotIn(z), "NotIn comparable true");
            Assert.False(x.NotIn(y), "NotIn comparable false");
            Assert.True(x.NotIn(defaultT), "NotIn comparable x with defaultT");
            Assert.True(defaultT.NotIn(x), "NotIn comparable defaultT with x");
            Assert.False(defaultT.NotIn(defaultT), "NotIn comparable defaultT with defaultT");

            Assert.True(x.In(yEnumerable), "In enumerable comparable true");
            Assert.False(x.In(zEnumerable), "In enumerable comparable false");
            Assert.False(x.In(defaultTEnumerable), "In enumerable comparable x with defaultT");
            Assert.False(defaultT.In(xEnumerable), "In enumerable comparable defaultT with x");
            Assert.True(defaultT.In(defaultTEnumerable), "In enumerable comparable defaultT with defaultT");

            Assert.True(x.NotIn(zEnumerable), "NotIn enumerable comparable true");
            Assert.False(x.NotIn(yEnumerable), "NotIn enumerable comparable false");
            Assert.True(x.NotIn(defaultTEnumerable), "NotIn enumerable comparable x with defaultT");
            Assert.True(defaultT.NotIn(xEnumerable), "NotIn enumerableotIn comparable defaultT with x");
            Assert.False(defaultT.NotIn(defaultTEnumerable), "NotIn enumerable comparable defaultT with defaultT");
        }

        private class PersonTest : Entity<PersonIdTest>
        {
            public PersonTest(PersonIdTest id)
                : base(id)
            {
            }

            public override string ToString() =>
                $"{Id}";

            public static PersonTest FromId(int id) =>
                new(id: new PersonIdTest { Id = id });
        }

        private class PersonIdTest : ValueObject
        {
            public int Id { get; init; }

            protected override IEnumerable<object> GetEqualityComponents()
            {
                yield return Id;
            }

            public override string ToString() =>
                $"{Id}";
        }

        private class SizeTest : EnumValueObject<SizeTest>
        {
            public static readonly SizeTest Small = new(nameof(Small), 160);

            public static readonly SizeTest Medium = new(nameof(Medium), 170);

            public static readonly SizeTest Large = new(nameof(Large), 180);

            public int Height { get; init; }

            public SizeTest(string id, int height) : base(id)
            {
                Height = height;
            }

            public override string ToString() =>
                $"{Id}|{Height}";
        }
    }
}
