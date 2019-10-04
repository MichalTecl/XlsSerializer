using System.Collections.Generic;

using XlsSerializer.Core.Attributes;

namespace XlsSerializer.UnitTests.TestModels
{
    [XlsSheet(1, "Title")]
    public class SheetWithCollectionOnAnotherSheet
    {
        [XlsCell("A1")]
        public string A { get; set; }

        [XlsCell("D2")]
        public int B { get; set; }

        [XlsSheet(2, "Collection1")]
        public List<string> Items1 { get; } = new List<string>();
        
        public static SheetWithCollectionOnAnotherSheet Setup()
        {
            var model = new SheetWithCollectionOnAnotherSheet()
            {
                A = "A1",
                B = 42
            };

            for (int i = 0; i < 10; i++)
            {
                model.Items1.Add($"item_{i}");
            }

            return model;
        }
    }
}
