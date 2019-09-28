using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using XlsSerializer.Core.Attributes;

namespace XlsSerializer.Examples.Public.MultipleSheetsModel
{
    public class SecondSheetModel
    {
        [XlsCell("A2")]
        public string CellA2 { get; set; }

        [XlsCell("B2")]
        public string CellB2 { get; set; }
    }
}
