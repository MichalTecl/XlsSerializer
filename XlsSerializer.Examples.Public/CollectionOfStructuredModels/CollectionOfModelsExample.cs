using System.Collections.Generic;
using System.IO;
using XlsSerializer.Core;
using XlsSerializer.Examples.Core;
using Xunit;

namespace XlsSerializer.Examples.Public.CollectionOfStructuredModels
{
    [ExampleTest(10, "Collection of custom objects")]
    public class CollectionOfModelsExample : ExampleTestBase
    {
        //#start_publishing
        protected override void Test(Stream target)
        {
            var model = new List<CollectionItem>();
            for (var i = 0; i < 100; i++)
            {
                model.Add(new CollectionItem
                {
                    Index = i,
                    ItemName = $"item_{i}"
                });
            }

            XlsxSerializer.Instance.Serialize(model, target);

            var deserialized = XlsxSerializer.Instance.Deserialize<List<CollectionItem>>(target);

            Assert.Equal(model, deserialized,
                CompareItems<CollectionItem>((x, y) => x.Index == y.Index && x.ItemName == y.ItemName));
        }
    }
}
