using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using XlsSerializer.Core.Attributes;

namespace XlsSerializer.Examples.Public.InheritedProperties
{
    public class Model : ModelBase
    {
        [XlsCell("A2")]
        public string Cell2 { get; set; }
    }
}
