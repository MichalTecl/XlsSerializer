using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;

using XlsSerializer.Core;
using XlsSerializer.Examples.Core;

namespace XlsSerializer.Examples.Public.Formulas
{
    [ExampleTest(777, "Formulas")]
    public class FormulasExample : ExampleTestBase
    {
        //#start_publishing
        protected override void Test(Stream target)
        {
            var model = new SheetWithFormulas
            {
                Value1 = 1,
                Value2 = 2
            };

            for (var i = 0; i < 10; i++)
            {
                model.Items.Add(new CollectionItemWithFormula
                {
                    Value1 = i,
                    Value2 = i * 2
                });
            }

            XlsxSerializer.Instance.Serialize(model, target);

            // There is nothing to assert, formulas are calculated when the workbook is opened in Excel
        }
    }
}
