using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using XlsSerializer.Core.Utils;
using Xunit;

namespace XlsSerializer.UnitTests
{
    public class ReflectionHelperTests
    {
        [Fact]
        public void TestPopulateCollectionProperty()
        {
            var testNumbers = new List<object> {42, 50, 60, 125, 15, 13};

            var target = new TestTarget();

            ReflectionHelper.PopulateCollectionProperty(target, target.GetType().GetProperty(nameof(TestTarget.CollectionGetSet)), testNumbers);
            ReflectionHelper.PopulateCollectionProperty(target, target.GetType().GetProperty(nameof(TestTarget.CollectionGet)), testNumbers);
            ReflectionHelper.PopulateCollectionProperty(target, target.GetType().GetProperty(nameof(TestTarget.ArrayGetSet)), testNumbers);

            AssertSame(testNumbers, target.CollectionGetSet);
            AssertSame(testNumbers, target.CollectionGet);
            AssertSame(testNumbers, target.ArrayGetSet);
        }

        [Theory]
        [InlineData(typeof(bool), true, null)]
        [InlineData(typeof(string), true, null)]
        [InlineData(typeof(int[]), false, null)]
        [InlineData(typeof(int[]), true, typeof(int))]
        [InlineData(typeof(List<string>), true, typeof(string))]
        [InlineData(typeof(List<string>), false, typeof(string))]
        public void TestGetIsCollection(Type type, bool acceptArray, Type expectedItemType)
        {
            var result = ReflectionHelper.GetIsCollection(type, out var itemType, acceptArray);
            if (expectedItemType == null)
            {
                Assert.False(result);
                Assert.Null(itemType);
            }
            else
            {
                Assert.True(result);
                Assert.Equal(expectedItemType, itemType);
            }
        }

        [Fact]
        public void TestConstructListOfType()
        {
            Assert.True(ReflectionHelper.ConstructListOf(typeof(int)) is List<int>);
        }

        internal static void AssertSame(IEnumerable a, IEnumerable b)
        {
            var itemsA = a.OfType<object>().ToList();
            var itemsB = b.OfType<object>().ToList();

            Assert.Equal(itemsA.Count, itemsB.Count);

            for (var i = 0; i < itemsA.Count; i++)
            {
                Assert.Equal(itemsA[i], itemsB[i]);
            }
        }

        public class TestTarget
        {
            public ICollection<int> CollectionGetSet { get; set; }

            public ICollection<int> CollectionGet { get; } = new List<int>();

            public int[] ArrayGetSet { get; set; }
        }
    }
}
