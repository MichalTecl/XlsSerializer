using XlsSerializer.Core.Attributes;

namespace XlsSerializer.UnitTests.TestModels
{
    public class TwoSheetsWorkbook
    {
        [XlsSheet(1, "FirstSheet")]
        public SimpleSheet FirstSheet { get; set; }

        [XlsSheet(2, "SecondSheet")]
        public SheetWithTwoComplexCollections SecondSheet { get; set; }

        public static TwoSheetsWorkbook Setup()
        {
            var model = new TwoSheetsWorkbook();
            model.FirstSheet = SimpleSheet.Setup();
            model.SecondSheet = SheetWithTwoComplexCollections.Setup();

            return model;
        }
    }
}
