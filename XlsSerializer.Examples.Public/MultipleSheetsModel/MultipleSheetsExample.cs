using System;
using System.IO;

using XlsSerializer.Core;
using XlsSerializer.Examples.Core;

using Xunit;

namespace XlsSerializer.Examples.Public.MultipleSheetsModel
{
    [ExampleTest(5, "model with Multiple Sheets")]
    public class MultipleSheetsExample : ExampleTestBase
    {
        //#start_publishing
        protected override void Test(Stream target)
        {
            var model = new WorkbookModel
            {
                FirstSheet = new FirstSheetModel
                {
                    CellA1 = Guid.NewGuid().ToString(),
                    CellB1 = Guid.NewGuid().ToString()
                },
                SecondSheet = new SecondSheetModel
                {
                    CellA2 = Guid.NewGuid().ToString(),
                    CellB2 = Guid.NewGuid().ToString()
                },
                ThirdSheet = new SecondSheetModel
                {
                    CellA2 = Guid.NewGuid().ToString(),
                    CellB2 = Guid.NewGuid().ToString()
                }
            };

            for (var i = 0; i < 10; i++)
            {
                model.CollectionSheet.Add(Guid.NewGuid().ToString());
            }

            XlsxSerializer.Instance.Serialize(model, target);

            var deserialized = XlsxSerializer.Instance.Deserialize<WorkbookModel>(target);

            Assert.Equal(model.FirstSheet.CellA1, deserialized.FirstSheet.CellA1);
            Assert.Equal(model.FirstSheet.CellB1, deserialized.FirstSheet.CellB1);

            Assert.Equal(model.SecondSheet.CellA2, deserialized.SecondSheet.CellA2);
            Assert.Equal(model.SecondSheet.CellB2, deserialized.SecondSheet.CellB2);

            Assert.Equal(model.ThirdSheet.CellA2, deserialized.ThirdSheet.CellA2);
            Assert.Equal(model.ThirdSheet.CellB2, deserialized.ThirdSheet.CellB2);

            Assert.Equal(model.CollectionSheet, deserialized.CollectionSheet);
        }
    }
}
