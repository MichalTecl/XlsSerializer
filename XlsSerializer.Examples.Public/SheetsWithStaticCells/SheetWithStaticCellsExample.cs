using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using XlsSerializer.Core;
using XlsSerializer.Examples.Core;

using Xunit;

namespace XlsSerializer.Examples.Public.SheetsWithStaticCells
{
    [ExampleTest(999, "Sheet with static cells")]
    public class SheetWithStaticCellsExample : ExampleTestBase
    {
        //#start_publishing
        protected override void Test(Stream target)
        {
            var model = new SheetModel
            {
                CellA1 = Guid.NewGuid().ToString(),
                CellA2 = 42,
                CellB1 = true
            };

            for (var i = 0; i < 10; i++)
            {
                model.Collection.Add($"item_{i}");
            }

            XlsxSerializer.Instance.Serialize(model, target);

            var deserialized = XlsxSerializer.Instance.Deserialize<SheetModel>(target);

            Assert.Equal(model.CellA1, deserialized.CellA1);
            Assert.Equal(model.CellA2, deserialized.CellA2);
            Assert.Equal(model.CellB1, deserialized.CellB1);
            Assert.Equal(model.Collection, deserialized.Collection);
        }

    }
}
