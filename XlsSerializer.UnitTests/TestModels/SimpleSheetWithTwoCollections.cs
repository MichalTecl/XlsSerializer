using System.Collections.Generic;
using XlsSerializer.Core.Attributes;

namespace XlsSerializer.UnitTests.TestModels
{
    public class SimpleSheetWithTwoCollections
    {
        [XlsCell(0, 0)]
        public List<string> ListA { get; } = new List<string>();

        [XlsCell(0, 1)]
        public List<string> ListB { get; } = new List<string>();

        public static SimpleSheetWithTwoCollections Generate()
        {
            var r = new SimpleSheetWithTwoCollections();

            for (var i = 0; i < 10; i++)
            {
                r.ListA.Add($"A_{i}");

                if ((i % 2) == 0)
                {
                    r.ListB.Add($"B_{i}");
                }
            }

            return r;
        }
    }
}
