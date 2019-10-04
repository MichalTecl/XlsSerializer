using System;

using OfficeOpenXml;

namespace XlsSerializer.Core.SettingsElements
{
    public interface IValueConverter
    {
        object ToCellValue(Type valueType, object input, ExcelRange cell);

        object FromCellValue(Type desiredType, object input);
    }
}
