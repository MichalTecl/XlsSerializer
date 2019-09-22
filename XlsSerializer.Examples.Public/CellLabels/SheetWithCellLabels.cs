using XlsSerializer.Core.Attributes;

namespace XlsSerializer.Examples.Public.CellLabels
{
    //Sets default label style for the sheet
    [LabelStyle(FontStyle = FontStyle.Italic, BackgroundColor = "Silver")]
    public class SheetWithCellLabels
    {
        [XlsCell("B1")]
        [Label("First Label")]
        public string Cell1 { get; set; }

        [XlsCell("C4")]
        [Label("Second Label", LabelLocation.Top)]
        //Sets label style for this property
        [LabelStyle(FontStyle = FontStyle.Strike, BackgroundColor = "#0000ff", Color = "White")]
        public string Cell2 { get; set; }
    }
}
