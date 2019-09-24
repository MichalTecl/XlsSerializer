using System;
using System.Globalization;

namespace XlsSerializer.Core.SettingsElements.Defaults
{
    public class DateStringConvertor : TypeConverterBase<DateTime>, ITypeConverter
    {
        public readonly string DateFormat;
        
        public DateStringConvertor() : this(CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern) { }

        public DateStringConvertor(string dateFormat)
        {
            DateFormat = dateFormat;
        }

        protected override object ConvertToCellValue(DateTime source)
        {
            return source.ToString(DateFormat);
        }

        protected override DateTime ConvertFromCellValue(object source)
        {
            return DateTime.ParseExact(source.ToString(), DateFormat, CultureInfo.InvariantCulture);
        }
    }
}
