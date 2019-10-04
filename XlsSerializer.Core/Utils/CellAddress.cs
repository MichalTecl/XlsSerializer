using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace XlsSerializer.Core.Utils
{
    public sealed class CellAddress
    {
        private const string c_columnChars = "abcdefghijklmnopqrstuvwxyz";

        private static readonly Regex s_charsRegex = new Regex("([A-z]+)");
        private static readonly Regex s_digitsRegex = new Regex("(\\d+)");

        public string Sheet { get; set; }
        public int? StartRow { get; set; }
        public int? StartColumn { get; set; }
        public int? EndRow { get; set; }
        public int? EndColumn { get; set; }

        public bool IsRange => (StartColumn != (EndColumn ?? StartColumn)) || (StartRow != (EndRow ?? StartRow));

        public static CellAddress Parse(string address)
        {
            address = address.Trim();

            var result = new CellAddress();
            
            var sheetAndAddress = address.Split('!');
            if (sheetAndAddress.Length == 2)
            {
                var sheet = sheetAndAddress[0].Trim().TrimStart('\'').TrimEnd('\'').Trim();
                address = sheetAndAddress[1];

                var range = Parse(address);
                range.Sheet = sheet;

                return range;
            }

            var fromAndTo = address.Split(':');
            if (fromAndTo.Length == 2)
            {
                var from = Parse(fromAndTo[0]);
                var to = Parse(fromAndTo[1]);

                result.StartColumn = from.StartColumn;
                result.StartRow = from.StartRow;
                result.EndColumn = to.StartColumn;
                result.EndRow = to.StartRow;

                return result;
            }

            var columnString = s_charsRegex.Matches(address).OfType<Match>().FirstOrDefault(m => !string.IsNullOrWhiteSpace(m.Value))?.Value;
            
            if (!string.IsNullOrWhiteSpace(columnString))
            {
                result.EndColumn = result.StartColumn = ColumnSymbolToIndex(columnString);

                var rowString = s_digitsRegex.Matches(address).OfType<Match>().FirstOrDefault(m => !string.IsNullOrWhiteSpace(m.Value))?.Value;
                if (!string.IsNullOrWhiteSpace(rowString) && int.TryParse(rowString, out var rowValue))
                {
                    result.EndRow = result.StartRow = rowValue - 1;
                }
            }
            
            return result;
        }

        public static int ColumnSymbolToIndex(string symbol)
        {
            symbol = symbol.ToLowerInvariant();

            var result = 0;
            for (var charPosition = 0; charPosition < symbol.Length; charPosition++)
            {
                var charValue = c_columnChars.IndexOf(symbol[charPosition]) + 1;
                var exponent = symbol.Length - charPosition - 1;

                result += (int) (Math.Pow(c_columnChars.Length, exponent) * charValue);
            }

            return result - 1;
        }
    }
}
