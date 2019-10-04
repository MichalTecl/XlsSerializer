using System;
using System.Reflection;

using OfficeOpenXml;

namespace XlsSerializer.Core.SettingsElements
{
    public interface ICellWriterInterceptor
    {
        void Write(object propertyOwner,
            PropertyInfo sourceProperty,
            Func<PropertyAndOwnerInstance, object> sourceValue,
            ExcelRange targetCell,
            Action<Type, object, ExcelRange, IValueConverter> defaultWriteAction,
            IValueConverter defaultConverter);
    }
}
