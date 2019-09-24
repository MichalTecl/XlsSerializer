using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using XlsSerializer.Core;
using XlsSerializer.Core.SettingsElements.Defaults;
using XlsSerializer.Examples.Core;

using Xunit;

namespace XlsSerializer.Examples.Public.CustomDateFormatExample
{
    [ExampleTest(5000, "Custom Date String format")]
    public class CustomDateFormatExample : ExampleTestBase
    {
        protected override void Test(Stream target)
        {
            var model = new ModelWithDateCells
            {
                Date1 = new DateTime(1984,9,1),
                Date2 = new DateTime(1989,7,7)
            };

            var serializer = new XlsxSerializer(new XlsxSerializerSettings(new DateStringConvertor("yyyy*MM*dd")));

            serializer.Serialize(model, target);

            var deserialized = serializer.Deserialize<ModelWithDateCells>(target);

            Assert.Equal(model.Date1, model.Date1);
            Assert.Equal(model.Date2, deserialized.Date2);
            Assert.Null(model.Date3);
        }
    }
}
