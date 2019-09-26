using OfficeOpenXml.Style;

using XlsSerializer.Core.Attributes;

namespace XlsSerializer.Examples.Public.CellLabels
{
    //#start_publishing
    //Sets default label style for the sheet
    [LabelStyle(FontStyle = FontStyle.Italic, BackgroundColor = "Silver")]
    public class SheetWithCellLabels
    {
        [XlsCell("B1")]
        [Label("First Label")]
        //Overwrites label style for the property
        [LabelStyle(HorizontalAlignment = ExcelHorizontalAlignment.Right, FontStyle = FontStyle.Strike, BackgroundColor = "#0000ff", Color = "White")]
        public string Cell1 { get; set; }

        [XlsCell("C4")]
        //Sets label style for this property
        [LabelStyle()]
        public string Cell2 { get; set; }
    }
}
