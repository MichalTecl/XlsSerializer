using System;

using XlsSerializer.Core.SettingsElements;

namespace XlsSerializer.Examples.Public.CustomValueConversion
{
    //#start_publishing
    public class CustomBoolTypeConverter : TypeConverterBase<bool?>
    {
        private const string c_yes = "YES";
        private const string c_no = "NO";
        private const string c_null = "Unknown";

        protected override object ConvertToCellValue(bool? source)
        {
            switch (source)
            {
                case true: return c_yes;
                case false: return c_no;
                default: return c_null;
            }
        }

        protected override bool? ConvertFromCellValue(object source)
        {
            var stringValue = source?.ToString().Trim();

            if (stringValue?.Equals(c_yes, StringComparison.InvariantCultureIgnoreCase) == true)
            {
                return true;
            }
            else if (stringValue?.Equals(c_no, StringComparison.InvariantCultureIgnoreCase) == true)
            {
                return false;
            }

            return null;
        }
    }
}
