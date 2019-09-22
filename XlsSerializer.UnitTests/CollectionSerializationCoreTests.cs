using System.Collections.Generic;
using System.Linq;
using XlsSerializer.Core.Features;
using XlsSerializer.UnitTests.TestModels;
using XlsSerializer.UnitTests.TestUtils;
using Xunit;

namespace XlsSerializer.UnitTests
{
    public class CollectionSerializationCoreTests
    {
        [Theory]
        [InlineData(0,0)]
        [InlineData(10, 0)]
        [InlineData(0, 10)]
        [InlineData(10, 10)]
        public void TestCollectionSerialization(int startRow, int startCol)
        {
            var sourceCollection = ComplexCollectionItemModel.Generate(100).ToList();
            List<ComplexCollectionItemModel> deserialized = null;

            ExcelPackageSaveAndLoad.SaveAndLoadSheet(sheetToWrite =>
                {
                    XlsCollectionSerializerCore.SerializeCollection(
                        typeof(ComplexCollectionItemModel), 
                        sourceCollection,
                        sheetToWrite, 
                        startRow, 
                        startCol);
                },
                sheetToLoad =>
                {
                    deserialized = XlsCollectionDeserializerCore.DeserializeCollection(
                            typeof(ComplexCollectionItemModel),
                            sheetToLoad, 
                            () => new ComplexCollectionItemModel(), 
                            startRow, 
                            startCol)
                        .OfType<ComplexCollectionItemModel>()
                        .ToList();
                }, $"collection_{startRow}_{startCol}.xlsx");

            Assert.NotNull(deserialized);

            Assert.Equal(sourceCollection.Count, deserialized.Count);

            for (var i = 0; i < sourceCollection.Count; i++)
            {
                var a = sourceCollection[i];
                var b = deserialized[i];

                Assert.Equal(a.Bool, b.Bool);
                Assert.Equal(a.Index, b.Index);
                Assert.Equal(a.Money1, b.Money1);
                Assert.Equal(a.Str1, b.Str1);
                Assert.Equal(a.Str2, b.Str2);
                Assert.Null(b.NotColumn);
            }
        }

        [Fact]
        public void TestPrimitiveCollectionSerialization()
        {
            var source = new List<string>();
            for (var i = 0; i < 10; i++)
            {
                source.Add($"item_{i + 1}");
            }

            List<string> deserialized = null;

            ExcelPackageSaveAndLoad.SaveAndLoadSheet(s =>
            {
                XlsCollectionSerializerCore.SerializeCollection(typeof(string), source, s, 0,0);
            },
                s =>
                {
                    deserialized = XlsCollectionDeserializerCore.DeserializeCollection(typeof(string), s, null, 0, 0)
                        .OfType<string>().ToList();
                });

            Assert.Equal(source.Count, deserialized.Count);

            for (var i = 0; i < source.Count; i++)
            {
                Assert.Equal(source[i], deserialized[i]);
            }
        }
    }
}
