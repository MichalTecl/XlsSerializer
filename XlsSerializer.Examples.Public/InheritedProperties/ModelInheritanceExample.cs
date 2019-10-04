using System;
using System.IO;

using XlsSerializer.Core;
using XlsSerializer.Examples.Core;

using Xunit;

namespace XlsSerializer.Examples.Public.InheritedProperties
{
    [ExampleTest(1000, "Inherited Model Properties")]
    public class ModelInheritanceExample : ExampleTestBase
    {
        //#start_publishing
        protected override void Test(Stream target)
        {
            var model = new Model
            {
                Cell1 = Guid.NewGuid().ToString(),
                Cell2 = Guid.NewGuid().ToString()
            };

            XlsxSerializer.Instance.Serialize(model, target);

            var deserialized = XlsxSerializer.Instance.Deserialize<Model>(target);

            Assert.Equal(model.Cell1, deserialized.Cell1);
            Assert.Equal(model.Cell2, deserialized.Cell2);
        }
    }
}
