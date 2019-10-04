using XlsSerializer.Core.Attributes;

namespace XlsSerializer.Examples.Public.InheritedProperties
{
    //#start_publishing
    public class Model : ModelBase
    {
        [XlsCell("A2")]
        public string Cell2 { get; set; }
    }
}
