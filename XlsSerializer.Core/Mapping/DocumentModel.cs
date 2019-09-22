using System;
using System.Collections.Generic;
using System.Linq;
using OfficeOpenXml;

namespace XlsSerializer.Core.Mapping
{
    internal sealed class DocumentModel
    {
        private readonly Lazy<bool> m_hasHeader;
        private readonly Lazy<int> m_startColumn;
        private readonly Lazy<int> m_endColumn;
        private readonly List<ICellBinding> m_bindings;

        public DocumentModel(IEnumerable<ICellBinding> items)
        {
            m_bindings = new List<ICellBinding>(items);
            m_hasHeader = new Lazy<bool>(() => m_bindings.Any(b => b.HasHeader));
            m_startColumn = new Lazy<int>(() => m_bindings.Any() ? m_bindings.Min(v => v.CellLocation.Column) : -1);
            m_endColumn = new Lazy<int>(() => m_bindings.Any() ? m_bindings.Max(v => v.CellLocation.Column) : -1);
        }

        public IEnumerable<ICellBinding> Values => m_bindings;

        public bool HasHeader => m_hasHeader.Value;
        public int StartColumn => m_startColumn.Value;
        public int EndColumn => m_endColumn.Value;

        private bool TryGetValue(CellLocation location, out ICellBinding binding)
        {
            binding = m_bindings.FirstOrDefault(b =>
                (b.CellLocation.Row == location.Row) && (b.CellLocation.Column == location.Column));

            return binding != null;
        }

        public void SetupHeader(int column, ExcelRange headerCell)
        {
            if (TryGetValue(new CellLocation(null, column), out var m))
            {
                m.SetupHeader(headerCell);
            }
        }
    }
}
