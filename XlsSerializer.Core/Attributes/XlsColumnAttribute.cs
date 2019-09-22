using System;
using XlsSerializer.Core.Attributes.Contract;
using XlsSerializer.Core.Utils;

namespace XlsSerializer.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class XlsColumnAttribute : XlsPropertyAttribute, IHasColumnIndex, IHasHeader
    {
        public XlsColumnAttribute(string address, string headerText = null, string numberFormat = null) : this(
            CellAddress.Parse(address), headerText, numberFormat)
        {
        }

        private XlsColumnAttribute(CellAddress address, string headerText, string numberFormat) : this(
            address.StartColumn ?? 0, headerText, numberFormat)
        {
            if (address.StartRow != null || address.StartColumn == null || !string.IsNullOrWhiteSpace(address.Sheet) || address.IsRange)
            {
                throw new ArgumentException("Invalid cell address - address of single column without row or sheet reference required");
            }
        }

        public XlsColumnAttribute(int columnIndex, string headerText = null, string numberFormat = null) : base(numberFormat)
        {
            ColumnIndex = columnIndex;
            HeaderText = headerText;
        }
        
        public int ColumnIndex { get; }
        public string HeaderText { get; }
        
    }
}
