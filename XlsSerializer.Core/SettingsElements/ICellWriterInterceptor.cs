using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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
