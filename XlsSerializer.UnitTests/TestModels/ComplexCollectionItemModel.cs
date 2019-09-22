using System;
using System.Collections.Generic;
using XlsSerializer.Core;
using XlsSerializer.Core.Attributes;

namespace XlsSerializer.UnitTests.TestModels
{
    public class ComplexCollectionItemModel : IHasSourceRowIndex
    {
        [XlsColumn("A", "First")]
        public int? Index { get; set; }

        [XlsColumn("B")]
        public string Str1 { get; set; }

        [XlsColumn("C", "Text")]
        public string Str2 { get; set; }

        [XlsColumn("D", "Money", "0.00")]
        public decimal Money1 { get; set; }

        public string NotColumn { get; set; }

        [XlsColumn("E", "Bool")]
        public bool Bool { get; set; }



        public static IEnumerable<ComplexCollectionItemModel> Generate(int count)
        {
            for (var i = 0; i < count; i++)
            {
                yield return new ComplexCollectionItemModel
                {
                    Index = (i % 2 == 1) ? (int?)null : i,
                    Str1 = Guid.NewGuid().ToString(),
                    Str2 = Guid.NewGuid().ToString(),
                    Money1 = ((decimal)i) / 100m,
                    NotColumn = Guid.NewGuid().ToString(),
                    Bool = i % 2 == 0
                };
            }
        }

        public int SourceRowIndex { get; set; }
    }
}
