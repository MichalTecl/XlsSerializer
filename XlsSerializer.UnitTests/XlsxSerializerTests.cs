using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XlsSerializer.UnitTests.TestModels;
using XlsSerializer.UnitTests.TestUtils;
using Xunit;

namespace XlsSerializer.UnitTests
{
    public class XlsxSerializerTests
    {
        [Fact]
        public void TestCollectionSerialization()
        {
            var model = new List<string>(100);

            for (var i = 0; i < 100; i++)
            {
                model.Add(Guid.NewGuid().ToString());
            }

            var deserialized = TestSerialization(model);

            Assert.Equal(model.Count, deserialized.Count);

            for (int i = 0; i < model.Count; i++)
            {
                Assert.Equal(model[i], deserialized[i]);
            }
        }

        [Fact]
        public void TestComplexCollectionSerialization()
        {
            var model = new List<MultiTypeCollectionItem>();

            for (var i = 0; i < 100; i++)
            {
                model.Add(new MultiTypeCollectionItem());
            }

            var deserialized = TestSerialization(model, "complexTypes.xlsx");

            Assert.Equal(model.Count, deserialized.Count);
        }

        [Fact]
        public void TestMultipleSheetsInSingleClass()
        {
            var model = SheetWithCollectionOnAnotherSheet.Setup();

            var deserialized = TestSerialization(model, "MultipleSheetsInSingleClasss.xlsx");

            Assert.Equal(model.A, deserialized.A);
            Assert.Equal(model.B, deserialized.B);
            Assert.Equal(model.Items1.Count, deserialized.Items1.Count);
        }

        [Fact]
        public void TestMultipleSheetsInSingleClassIncludingHidden()
        {
            var model = SheetWithCollectionOnAnotherHiddenSheet.Setup();

            var deserialized = TestSerialization(model, "MultipleSheetsInSingleClasssWithHidden.xlsx");

            Assert.Equal(model.A, deserialized.A);
            Assert.Equal(model.B, deserialized.B);
            Assert.Equal(model.Items1.Count, deserialized.Items1.Count);
        }

        private static T TestSerialization<T>(T model, string outputFile = null) where T:new()
        {
            using (var stream = new MemoryStream())
            {
                XlsSerializer.Core.XlsxSerializer.Instance.Serialize(model, stream);

                stream.Position = 0;

                if (!string.IsNullOrWhiteSpace(outputFile))
                {
                    File.WriteAllBytes(outputFile, stream.GetBuffer());
                }

                stream.Position = 0;

                return XlsSerializer.Core.XlsxSerializer.Instance.Deserialize<T>(stream);
            }
        }
    }
}
