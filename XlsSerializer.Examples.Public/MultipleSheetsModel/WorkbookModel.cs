using System.Collections.Generic;
using XlsSerializer.Core.Attributes;

namespace XlsSerializer.Examples.Public.MultipleSheetsModel
{
    //#start_publishing
    public class WorkbookModel
    {
        [XlsSheet(0, "First Sheet")]
        public FirstSheetModel FirstSheet { get; set; }

        [XlsSheet(1, "Second Sheet")]
        public SecondSheetModel SecondSheet { get; set; }

        [XlsSheet(2, "Third sheet")]
        public SecondSheetModel ThirdSheet { get; set; }

        [XlsSheet(3, "Collection Sheet")]
        public List<string> CollectionSheet { get; } = new List<string>();
    }
}
