using System;
using System.IO;
using System.Linq;

using XlsSerializer.Core;
using XlsSerializer.Examples.Core;

namespace XlsSerializer.Examples.Public.ListValidation
{
    [ExampleTest(999, "List validation")]
    public class ListValidationExample : ExampleTestBase
    {
        //#start_publishing
        protected override void Test(Stream target)
        {
            var model = new SheetWithListValidation();
            model.Colors.AddRange(Enum.GetNames(typeof(System.Drawing.KnownColor)).OrderBy(c => c));

            XlsxSerializer.Instance.Serialize(model, target);
        }
    }
}
