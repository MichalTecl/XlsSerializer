using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using OfficeOpenXml;
using XlsSerializer.Core.Features;
using XlsSerializer.UnitTests.TestModels;
using XlsSerializer.UnitTests.TestUtils;
using Xunit;

namespace XlsSerializer.UnitTests
{
    public partial class SheetSerialiaztionCoreTests
    {
        [Fact]
        public void TestSheetSerialization()
        {
            var model = new SimpleSheet()
            {
                Logic = true,
                Text = Guid.NewGuid().ToString(),
                Number = 42
            };

            for (var i = 0; i < 100; i++)
            {
                model.Items.Add(new CollectionItem
                {
                    Index = i,
                    Value = $"Item{i}"
                });
            }

            using (var excelPackage = new ExcelPackage())
            {
                var sheet = excelPackage.Workbook.Worksheets.Add("Sheet1");

                XlsSheetSerializerCore.Serialize(typeof(SimpleSheet), model, sheet);

                excelPackage.SaveAs(new FileInfo("testsheet1.xlsx"));
            }

            var deserialized = new SimpleSheet();
            using (var excelPackage = new ExcelPackage(new FileInfo("testsheet1.xlsx")))
            {
               XlsSheetDeserializerCore.Deserialize(deserialized, excelPackage.Workbook.Worksheets["Sheet1"]);
            }

            Assert.Equal(model.Logic, deserialized.Logic);
            Assert.Equal(model.Number, deserialized.Number);
            Assert.Equal(model.Text, deserialized.Text);

            Assert.Equal(model.Items.Count, deserialized.Items.Count);

            for (var i = 0; i < model.Items.Count; i++)
            {
                var modelItem = model.Items[i];
                var deseItem = deserialized.Items[i];

                Assert.Equal(modelItem.Index, deseItem.Index);
                Assert.Equal(modelItem.Value, deseItem.Value);
            }
        }

        [Fact]
        public void TestPrimitiveCollectionSerialization()
        {
            var model = SimpleSheetWithPrimitiveCollection.Generate(100);
            var deserialized = new SimpleSheetWithPrimitiveCollection();

            ExcelPackageSaveAndLoad.SaveAndLoadSheet(s =>
            {
                XlsSheetSerializerCore.Serialize(typeof(SimpleSheetWithPrimitiveCollection), model, s);
            }, s =>
            {
                XlsSheetDeserializerCore.Deserialize(deserialized, s);
            }, "withPrimitiveCollection.xlsx");

            Assert.Equal(model.Text, deserialized.Text);
            Assert.Equal(model.StringItems.Length, deserialized.StringItems.Length);

            for (var i = 0; i < model.StringItems.Length; i++)
            {
                Assert.Equal(model.StringItems[i], deserialized.StringItems[i]);
            }
        }

        [Fact]
        public void TestSerializationOfTwoCollections()
        {
            var source = SimpleSheetWithTwoCollections.Generate();
            var deserialized = new SimpleSheetWithTwoCollections();

            ExcelPackageSaveAndLoad.SaveAndLoadSheet(s =>
            {
                XlsSheetSerializerCore.Serialize(typeof(SimpleSheetWithTwoCollections), source, s);

            }, s =>
            {
                XlsSheetDeserializerCore.Deserialize(deserialized, s);
            }, 
                "twocollections.xlsx");

            Assert.Equal(string.Join(",", source.ListA), string.Join(",", deserialized.ListA));
            Assert.Equal(string.Join(",", source.ListB), string.Join(",", deserialized.ListB));
        }

        [Fact]
        public void TestSerializaionOfTwoComplexCollections()
        {
            var obj = new SheetWithTwoComplexCollections();
            
            for (var i = 0; i < 100; i++)
            {
                obj.List1.Add(new CollectionItem()
                {
                    Index = i,
                    Value = $"item_{i}"
                });
            }

            obj.Array1 = obj.List1.Where(i => (i.Index % 2) == 0).ToArray();

            var deserialized = new SheetWithTwoComplexCollections();

            ExcelPackageSaveAndLoad.SaveAndLoadSheet(
                s => { XlsSheetSerializerCore.Serialize(typeof(SheetWithTwoComplexCollections), obj, s); },
                s =>
                {
                    XlsSheetDeserializerCore.Deserialize(deserialized, s);
                }, "twocompcol.xlsx");

            Assert.Equal(JsonConvert.SerializeObject(obj), JsonConvert.SerializeObject(deserialized));
        }
    }
}
