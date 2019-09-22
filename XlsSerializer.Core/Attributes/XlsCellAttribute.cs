using System;
using System.Collections;
using System.Reflection;
using OfficeOpenXml;
using XlsSerializer.Core.Attributes.Contract;
using XlsSerializer.Core.Features;
using XlsSerializer.Core.Utils;

namespace XlsSerializer.Core.Attributes
{
    public sealed class XlsCellAttribute : XlsPropertyAttribute, IHasColumnIndex, IHasRowIndex
    {
        public XlsCellAttribute(string cellAddress, string numberFormat = null) : this(CellAddress.Parse(cellAddress), numberFormat) { }

        private XlsCellAttribute(CellAddress address, string format) : this(address.StartRow ?? 0,
            address.StartColumn ?? 0, format)
        {
            if (address.StartRow == null || address.StartColumn == null || !string.IsNullOrWhiteSpace(address.Sheet) || address.IsRange)
            {
                throw new ArgumentException("Invalid cell address - full address of single cell without sheet reference required");
            }
        }

        public XlsCellAttribute(int row, int column, string numberFormat = null):base(numberFormat)
        {
            ColumnIndex = column;
            RowIndex = row;
        }

        public int ColumnIndex { get; }
        public int RowIndex { get; }

        public override void WriteCellValue(PropertyInfo sourceProperty, object owner, ExcelRange cell)
        {
            if (ReflectionHelper.GetIsCollection(sourceProperty.PropertyType, out var collectionItemType, true))
            {
                var value = sourceProperty.GetValue(owner, null) as IEnumerable ?? new object[0];
                XlsCollectionSerializerCore.SerializeCollection(collectionItemType, value, cell.Worksheet, cell.Start.Row - 1, cell.Start.Column - 1);
                return;
            }

            base.WriteCellValue(sourceProperty, owner, cell);
        }
    }
}
