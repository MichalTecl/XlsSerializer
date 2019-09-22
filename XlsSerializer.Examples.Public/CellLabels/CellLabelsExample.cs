using System;
using System.IO;

using XlsSerializer.Core;
using XlsSerializer.Examples.Core;

using Xunit;

namespace XlsSerializer.Examples.Public.CellLabels
{
    [ExampleTest(1000, "Cell Labels")]
    public class CellLabelsExample : ExampleTestBase
    {
        protected override void Test(Stream target)
        {
            var model = new SheetWithCellLabels
            {
                Cell1 = Guid.NewGuid().ToString(),
                Cell2 = Guid.NewGuid().ToString()
            };

            XlsxSerializer.Instance.Serialize(model, target);

            var deserialized = XlsxSerializer.Instance.Deserialize<SheetWithCellLabels>(target);

            Assert.Equal(model.Cell1, deserialized.Cell1);
            Assert.Equal(model.Cell2, deserialized.Cell2);
        }
    }
}
