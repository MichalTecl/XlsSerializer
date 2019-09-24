using System;
using System.Reflection;

using OfficeOpenXml;

namespace XlsSerializer.Core.SettingsElements.Defaults
{
    internal sealed class DefaultCellWriterInterceptor : ICellWriterInterceptor
    {
        public static ICellWriterInterceptor Instance = new DefaultCellWriterInterceptor();

        public void Write(object propertyOwner,
            PropertyInfo sourceProperty,
            Func<PropertyAndOwnerInstance, object> sourceValue,
            ExcelRange targetCell,
            Action<Type, object, ExcelRange, IValueConverter> defaultWriteAction,
            IValueConverter defaultConverter)
        {
            var value = sourceValue(new PropertyAndOwnerInstance(sourceProperty, propertyOwner));

            defaultWriteAction(sourceProperty.PropertyType, value, targetCell, defaultConverter);
        }
    }
}
