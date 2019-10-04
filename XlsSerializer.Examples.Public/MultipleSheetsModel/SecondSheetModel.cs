using XlsSerializer.Core.Attributes;

namespace XlsSerializer.Examples.Public.MultipleSheetsModel
{
    public class SecondSheetModel
    {
        [XlsCell("A2")]
        public string CellA2 { get; set; }

        [XlsCell("B2")]
        public string CellB2 { get; set; }
    }
}
