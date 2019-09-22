using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XlsSerializer.Core.Attributes;

namespace XlsSerializer.UnitTests.TestModels
{
    public class MultiTypeCollectionItem
    {
        private static readonly Random s_rnd = new Random();

        [XlsColumn(1, "Field1")] public int C1 { get; set; } = s_rnd.Next();

        [XlsColumn(2, "Field2")] public int? C2 { get; set; } = (s_rnd.Next() % 2) == 0 ? (int?)s_rnd.Next() : null;

        [XlsColumn(3)]
        public string C3 { get; set; } = Guid.NewGuid().ToString();

        //[XlsColumn(4, numberFormat: "yyyy-MM-dd")]
        //public DateTime C4 { get; set; } = DateTime.Now;

        //[XlsColumn(5, numberFormat: "yyyy-MM-dd")]
        //public DateTime? C5 { get; set; } = (s_rnd.Next() % 2) == 0 ? (DateTime?)DateTime.Now : null;

        [XlsColumn(6)] public bool C6 { get; set; } = (s_rnd.Next() % 2) == 0;

        [XlsColumn(7)] public bool? C7 { get; set; } = (s_rnd.Next() % 2) == 0 ? (bool?) ((s_rnd.Next() % 2) == 0) : null;

        //[XlsColumn(8)] public TimeSpan C8 { get; set; } = DateTime.Now - DateTime.Today;

        //[XlsColumn(9)]
        //public TimeSpan? C9 { get; set; } = (s_rnd.Next() % 2) == 0 ? (TimeSpan?) (DateTime.Now - DateTime.Now) : null;

        [XlsColumn(10)] public decimal C10 { get; set; } = (decimal)s_rnd.NextDouble();

        [XlsColumn(11)]
        public decimal? C11 { get; set; } = (s_rnd.Next() % 2) == 0 ? (decimal?)s_rnd.NextDouble() : null;

        //[XlsColumn(12)]
        //public StringSplitOptions C12 { get; set; } =
        //    (s_rnd.Next() % 2) == 0 ? StringSplitOptions.RemoveEmptyEntries : StringSplitOptions.None;
    }
}
