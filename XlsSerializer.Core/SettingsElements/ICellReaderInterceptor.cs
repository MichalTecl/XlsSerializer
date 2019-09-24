using System;
using System.Reflection;

using OfficeOpenXml;

namespace XlsSerializer.Core.SettingsElements
{
    public interface ICellReaderInterceptor
    {
        void ReadCell(ExcelRange cell,
            object propertyOwner,
            PropertyInfo targetProperty,
            Func<PropertyAndOwnerInstance, bool> defaultShouldProcessCellDecision,
            Func<ExcelRange, Type, PropertyAndOwnerInstance, IValueConverter, object> defaultConvertCellValueToType,
            Action<PropertyAndOwnerInstance, object> defaultPropertySetter,
            IValueConverter defaultValueConverter);
    }
}
