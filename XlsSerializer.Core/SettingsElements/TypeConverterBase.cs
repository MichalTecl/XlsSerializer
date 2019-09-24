using System;

namespace XlsSerializer.Core.SettingsElements
{
    public abstract class TypeConverterBase<T> : ITypeConverter
    {
        public Type ConvertedType => typeof(T);

        public object ToCellValue(object input)
        {
            return ConvertToCellValue(input == null ? default(T) : (T)input);
        }

        public object FromCellValue(Type desiredType, object input)
        {
            return ConvertFromCellValue(input);
        }

        protected abstract object ConvertToCellValue(T source);

        protected abstract T ConvertFromCellValue(object source);
    }
}
