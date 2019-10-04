using System.IO;

using XlsSerializer.Core;
using XlsSerializer.Examples.Core;

namespace XlsSerializer.Examples.Public.XlsSerializerInstantiation
{
    [ExampleTest(0, "Serializer object instantiation")]
    public class XlsSerializerInstantiationExample : ExampleTestBase
    {
        //#start_publishing
        protected override void Test(Stream target)
        {
            // You can use a singleton instance:
            IXlsxSerializer serializer = XlsxSerializer.Instance;

            // Or instantiate the class:
            serializer = new XlsxSerializer();
        }
    }
}
