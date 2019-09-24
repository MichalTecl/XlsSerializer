using System;

namespace XlsSerializer.Core.SettingsElements
{
    public interface ITypeConverter 
    {
        Type ConvertedType { get; }

        object ToCellValue(object input);

        object FromCellValue(Type desiredType, object input);
    }
}
