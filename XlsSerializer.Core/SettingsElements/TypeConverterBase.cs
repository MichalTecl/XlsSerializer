using System;

using OfficeOpenXml;

namespace XlsSerializer.Core.SettingsElements
{
    public abstract class TypeConverterBase<T> : ITypeConverter
    {
        public Type ConvertedType => typeof(T);

        public object ToCellValue(object input, ExcelRange cell)
        {
            return ConvertToCellValue(input == null ? default(T) : (T)input, cell);
        }

        public object FromCellValue(Type desiredType, object input)
        {
            return ConvertFromCellValue(input);
        }

        protected abstract object ConvertToCellValue(T source, ExcelRange cell);

        protected abstract T ConvertFromCellValue(object source);
    }
}
