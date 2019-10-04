using System;

using OfficeOpenXml;

namespace XlsSerializer.Core.SettingsElements
{
    public interface ITypeConverter 
    {
        Type ConvertedType { get; }

        object ToCellValue(object input, ExcelRange cell);

        object FromCellValue(Type desiredType, object input);
    }
}
