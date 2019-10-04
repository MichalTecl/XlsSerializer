using System;
using System.Linq;

using OfficeOpenXml;

using XlsSerializer.Core.SettingsElements;
using XlsSerializer.Core.SettingsElements.Defaults;

namespace XlsSerializer.Core
{
    public class XlsxSerializerSettings : IValueConverter
    {
        private readonly IValueConverterBuilder m_valueConverter;

        public XlsxSerializerSettings(params ITypeConverter[] converter)
        {
            if (converter.Any())
            {
                m_valueConverter = ValueConverter.Default.RegisterTypeConverter(converter);
            }
            else
            {
                m_valueConverter = ValueConverter.Default;
            }

        }

        public ICellWriterInterceptor CellWriterInterceptor { get; set; } = DefaultCellWriterInterceptor.Instance;

        public ICellReaderInterceptor CellReaderInterceptor { get; set; } = DefaultCellReaderInterceptor.Instance;
        
        public object ToCellValue(Type valueType, object input, ExcelRange cell)
        {
            return m_valueConverter.ToCellValue(valueType, input, cell);
        }

        public object FromCellValue(Type desiredType, object input)
        {
            return m_valueConverter.FromCellValue(desiredType, input);
        }
    }
}
