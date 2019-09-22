using Newtonsoft.Json;
using XlsSerializer.Core.Features;
using XlsSerializer.UnitTests.TestModels;
using XlsSerializer.UnitTests.TestUtils;
using Xunit;

namespace XlsSerializer.UnitTests
{
    public class XlsWorkbookSerializerTests
    {
        [Fact]
        public void Test()
        {
            var wbModel = TwoSheetsWorkbook.Setup();

            var deserialized = new TwoSheetsWorkbook();

            ExcelPackageSaveAndLoad.WorkbookTest(wb => { XlsWorkbookSerializerCore.SerializeWorkbook(wbModel, wb); },
                wb =>
                {
                    XlsWorkbookDeserializerCore.DeserializeWorkbook(deserialized, wb);
                }, 
                "workbook1.xlsx");

            Assert.Equal(JsonConvert.SerializeObject(wbModel), JsonConvert.SerializeObject(deserialized));
        }

        [Fact]
        public void Test2()
        {
            var model = new ListValidationCase();

            ExcelPackageSaveAndLoad.WorkbookTest(wb => { XlsWorkbookSerializerCore.SerializeWorkbook(model, wb); },
                wb =>
                {
                },
                "validation1.xlsx");

        }
    }
}
