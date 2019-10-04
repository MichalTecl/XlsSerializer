using System;

using OfficeOpenXml;

namespace XlsSerializer.Core.SettingsElements.Defaults
{
    public class DateConvertor : TypeConverterBase<DateTime>, ITypeConverter
    {
        private const string c_defaultDateFormat = "mm-dd-yy";
        private static readonly DateTime s_base = new DateTime(1900, 1, 1);
        
        protected override object ConvertToCellValue(DateTime source, ExcelRange cell)
        {
            cell.Style.Numberformat.Format = c_defaultDateFormat;
            return (source - s_base).Days + 2;
        }

        protected override DateTime ConvertFromCellValue(object source)
        {
            if (source is DateTime dt)
            {
                return dt;
            }

            if (!(source is double decDate))
            {
                return s_base;
            }

            return DateTime.FromOADate(decDate);
        }
    }
}
