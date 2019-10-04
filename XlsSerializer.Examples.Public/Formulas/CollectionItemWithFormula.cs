using XlsSerializer.Core.Attributes;

namespace XlsSerializer.Examples.Public.Formulas
{
    //#start_publishing
    public class CollectionItemWithFormula
    {
        [XlsColumn("A")]
        public int Value1 { get; set; }

        [XlsColumn("B")]
        public int Value2 { get; set; }

        [XlsColumn("C")]
        [R1C1Formula("R[0]C[-2] + R[0]C[-1]")]
        public int RowSum { get; set; }
    }
}
