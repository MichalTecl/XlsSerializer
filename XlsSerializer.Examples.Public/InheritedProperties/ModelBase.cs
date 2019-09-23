using XlsSerializer.Core.Attributes;

namespace XlsSerializer.Examples.Public.InheritedProperties
{
    public abstract class ModelBase 
    {
        [XlsCell("A1")]
        public string Cell1 { get; set; }
    }
}
