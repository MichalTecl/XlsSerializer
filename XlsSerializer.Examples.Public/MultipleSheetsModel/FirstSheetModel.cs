using XlsSerializer.Core.Attributes;

namespace XlsSerializer.Examples.Public.MultipleSheetsModel
{
    public class FirstSheetModel
    {
        [XlsCell("A1")]
        public string CellA1 { get; set; }

        [XlsCell("B1")]
        public string CellB1 { get; set; }
    }
}
