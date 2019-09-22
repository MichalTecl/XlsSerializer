using System;
using XlsSerializer.Core.Attributes;

namespace XlsSerializer.UnitTests.TestModels
{
    public class SimpleSheetWithPrimitiveCollection
    {
        [XlsCell(0, 0)]
        public string Text { get; set; }

        [XlsCell(0, 1)]
        public string[] StringItems { get; set; }

        public static SimpleSheetWithPrimitiveCollection Generate(int collectionCount)
        {
            var result = new SimpleSheetWithPrimitiveCollection
            {
                Text = Guid.NewGuid().ToString()
            };

            result.StringItems = new string[collectionCount];

            for (var i = 0; i < collectionCount; i++)
            {
                result.StringItems[i] = $"item_{i}";
            }

            return result;
        }
    }
}
