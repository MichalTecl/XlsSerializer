using System.Collections.Generic;
using XlsSerializer.Core.Attributes;

namespace XlsSerializer.UnitTests.TestModels
{
    [HeaderStyle(FontStyle = FontStyle.Bold)]
    public class CollectionItem
    {
        [HeaderStyle(FontStyle = FontStyle.Italic)]
        [XlsColumn(0, "Index")]
        public int Index { get; set; }

        [XlsColumn(1, "Value")]
        public string Value { get; set; }

        [R1C1Formula("R[0]C[-2]*2")]
        [XlsColumn(2, "ValueTwice")]
        public int Calculated { get; set; }

        [XlsColumn("D", "Nullable")]
        public int? NullbableValue { get; set; }

        public static ICollection<CollectionItem> Generate(int count, ICollection<CollectionItem> target = null)
        {
            target = target ?? new List<CollectionItem>(count);
            for (var i = 0; i < count; i++)
            {
               target.Add(new CollectionItem
                {
                    Index = i,
                    Value = $"item_{i}",
                    NullbableValue = (i % 2) == 0 ? (int?)null : i
                });
            }

            return target;
        }
    }
}
