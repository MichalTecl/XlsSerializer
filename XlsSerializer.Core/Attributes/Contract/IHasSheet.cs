using OfficeOpenXml;

namespace XlsSerializer.Core.Attributes.Contract
{
    public interface IHasSheet
    {
        string SheetName { get; }

        int SheetOrder { get; }

        void SetupSheet(ExcelWorksheet sheet);
    }
}
