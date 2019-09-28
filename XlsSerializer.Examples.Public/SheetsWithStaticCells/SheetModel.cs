using System.Collections.Generic;

using XlsSerializer.Core.Attributes;

namespace XlsSerializer.Examples.Public.SheetsWithStaticCells
{
    //#start_publishing
    public class SheetModel
    {
        [XlsCell("A1")]
        public string CellA1 { get; set; }

        [XlsCell("A2")]
        public int CellA2 { get; set; }

        [XlsCell("B1")]
        public bool CellB1 { get; set; }

        [XlsCell("A3")]
        public List<string> Collection { get; } = new List<string>();
    }
}
