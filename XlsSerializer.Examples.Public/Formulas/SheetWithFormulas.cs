using System.Collections.Generic;

using XlsSerializer.Core.Attributes;

namespace XlsSerializer.Examples.Public.Formulas
{
    //#start_publishing
    public class SheetWithFormulas
    {
        [XlsCell("A1")]
        public int Value1 { get; set; }

        [XlsCell("B1")]
        public int Value2 { get; set; }

        [XlsCell("C1")]
        [Formula("A1+A2")]
        public int Value3 { get; set; }

        [XlsCell("A3")]
        public List<CollectionItemWithFormula> Items { get; } = new List<CollectionItemWithFormula>();
    }
}
