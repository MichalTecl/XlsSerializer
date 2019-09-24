using System;

using XlsSerializer.Core.Attributes;

namespace XlsSerializer.Examples.Public.CustomDateFormatExample
{
    public class ModelWithDateCells
    {
        [XlsCell("A1")]
        public DateTime Date1 { get; set; }

        [XlsCell("A2")]
        public DateTime? Date2 { get; set; }

        public DateTime? Date3 { get; set; }
    }
}
