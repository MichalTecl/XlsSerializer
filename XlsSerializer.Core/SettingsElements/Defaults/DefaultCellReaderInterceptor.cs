using System;
using System.Reflection;

using OfficeOpenXml;

namespace XlsSerializer.Core.SettingsElements.Defaults
{
    internal sealed class DefaultCellReaderInterceptor : ICellReaderInterceptor
    {
        public static ICellReaderInterceptor Instance = new DefaultCellReaderInterceptor();

        public void ReadCell(ExcelRange cell,
            object propertyOwner,
            PropertyInfo targetProperty,
            Func<PropertyAndOwnerInstance, bool> defaultShouldProcessCellDecision,
            Func<ExcelRange, Type, PropertyAndOwnerInstance, IValueConverter, object> defaultConvertCellValueToType,
            Action<PropertyAndOwnerInstance, object> defaultPropertySetter,
            IValueConverter defaultValueConverter)
        {
            var pao = new PropertyAndOwnerInstance(targetProperty, propertyOwner);

            if (!defaultShouldProcessCellDecision(pao))
            {
                return;
            }

            var cellValue = defaultConvertCellValueToType(cell, targetProperty.PropertyType, pao, defaultValueConverter);

            if (cellValue != null)
            {
                defaultPropertySetter(pao, cellValue);
            }
        }
    }
}
