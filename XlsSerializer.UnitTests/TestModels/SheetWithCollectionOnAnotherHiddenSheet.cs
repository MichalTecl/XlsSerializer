using System.Collections.Generic;

using XlsSerializer.Core.Attributes;

namespace XlsSerializer.UnitTests.TestModels
{
    public class SheetWithCollectionOnAnotherHiddenSheet
    {
        [XlsCell(0,0)]
        public string A { get; set; }

        [XlsCell(1,0)]
        public int B { get; set; }

        [XlsSheet(2, "HiddenCollection", IsHidden = true)]
        public List<string> Items1 { get; } = new List<string>();
        
        public static SheetWithCollectionOnAnotherHiddenSheet Setup()
        {
            var model = new SheetWithCollectionOnAnotherHiddenSheet()
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
