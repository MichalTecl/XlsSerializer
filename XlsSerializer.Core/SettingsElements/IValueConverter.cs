using System;

namespace XlsSerializer.Core.SettingsElements
{
    public interface IValueConverter
    {
        object ToCellValue(Type valueType, object input);

        object FromCellValue(Type desiredType, object input);
    }
}
