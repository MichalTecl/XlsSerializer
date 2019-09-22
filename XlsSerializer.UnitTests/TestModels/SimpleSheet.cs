using System;
using System.Collections.Generic;
using XlsSerializer.Core.Attributes;

namespace XlsSerializer.UnitTests.TestModels
{
    public class SimpleSheet
    {
        [XlsCell(0, 0)]
        public int Number { get; set; }

        [XlsCell(1, 1)]
        public string Text { get; set; }

        [XlsCell(2, 2)]
        public bool Logic { get; set; }

        [XlsCell(3, 0)]
        public List<CollectionItem> Items { get; } = new List<CollectionItem>();

        public static SimpleSheet Setup()
        {
            var obj = new SimpleSheet
            {
                Number = DateTime.Now.Millisecond,
                Text = Guid.NewGuid().ToString(),
                Logic = (DateTime.Now.Millisecond % 2) == 0
            };

            CollectionItem.Generate(10, obj.Items);

            return obj;
        }
    }
}
