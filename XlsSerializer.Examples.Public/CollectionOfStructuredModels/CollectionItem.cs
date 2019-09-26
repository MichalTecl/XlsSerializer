using XlsSerializer.Core.Attributes;

namespace XlsSerializer.Examples.Public.CollectionOfStructuredModels
{
    //#start_publishing
    public class CollectionItem
    {
        //Please note that column index in the XlsColumnAttribute is zero-based
        [XlsColumn(0)]
        public int Index { get; set; }

        //Column char could be used as well
        [XlsColumn("B")]
        public string ItemName { get; set; }
    }
}
