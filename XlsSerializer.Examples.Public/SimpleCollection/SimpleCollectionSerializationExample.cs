using System;
using System.Collections.Generic;
using System.IO;
using XlsSerializer.Core;
using XlsSerializer.Examples.Core;
using Xunit;

namespace XlsSerializer.Examples.Public.SimpleCollection
{
    [ExampleTest(1, "Simple Collection Serialization")]
    public class SimpleCollectionSerializationExample:ExampleTestBase
    {
        protected override void Test(Stream target)
        {
            var model = new List<string>(100);

            for (var i = 0; i < 100; i++)
            {
                model.Add(Guid.NewGuid().ToString());
            }

            XlsxSerializer.Instance.Serialize(model, target);

            var deserialized = XlsxSerializer.Instance.Deserialize<List<string>>(target);

            Assert.Equal(model, deserialized);
        }
    }
}
