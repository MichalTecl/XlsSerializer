using System;
using OfficeOpenXml;
using XlsSerializer.Core.Attributes.Contract;

namespace XlsSerializer.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class)]
    public class XlsSheetAttribute : Attribute, IHasSheet
    {
        public XlsSheetAttribute(int sheetOrder) : this(sheetOrder, $"Sheet{sheetOrder}") { }

        public XlsSheetAttribute(int sheetOrder, string sheetName)
        {
            SheetName = sheetName;
            SheetOrder = sheetOrder;
        }

        public string SheetName { get; }

        public int SheetOrder { get; }
        public void SetupSheet(ExcelWorksheet sheet)
        {
            sheet.Hidden = IsHidden ? eWorkSheetHidden.Hidden : eWorkSheetHidden.Visible;
        }

        public bool IsHidden { get; set; }
    }
}
