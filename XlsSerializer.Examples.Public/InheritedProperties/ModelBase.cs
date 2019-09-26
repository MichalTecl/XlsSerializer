using XlsSerializer.Core.Attributes;

namespace XlsSerializer.Examples.Public.InheritedProperties
{
    //#start_publishing
    public abstract class ModelBase 
    {
        [XlsCell("A1")]
        public string Cell1 { get; set; }
    }
}
