using System.Collections.Generic;
using System.Linq;
using XlsSerializer.Core.Attributes;

namespace XlsSerializer.UnitTests.TestModels
{
    public class SheetWithTwoComplexCollections
    {
        [XlsCell(0, 0)]
        public List<CollectionItem> List1 { get; } = new List<CollectionItem>();

        [XlsCell(20,4)]
        public CollectionItem[] Array1 { get; set; }

        public static SheetWithTwoComplexCollections Setup()
        {
            var obj = new SheetWithTwoComplexCollections();

            CollectionItem.Generate(15, obj.List1);
            obj.Array1 = CollectionItem.Generate(20).ToArray();

            return obj;
        }


    }
}