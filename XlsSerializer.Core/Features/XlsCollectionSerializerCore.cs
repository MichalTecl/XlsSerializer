using System;
using System.Collections;
using OfficeOpenXml;

namespace XlsSerializer.Core.Features
{
    internal static class XlsCollectionSerializerCore
    {
        public static void SerializeCollection(Type itemType, IEnumerable collection, ExcelWorksheet target, int startRow, int startColumn, XlsxSerializerSettings settings)
        {
            var mapping = DocumentMapper.GetMapping(itemType);

            var sheetRow = 1 + startRow;

            if (mapping.HasHeader)
            {
                for (var i = mapping.StartColumn; i <= mapping.EndColumn; i++)
                {
                    var headerCell = target.Cells[sheetRow, i + 1 + startColumn];
                    mapping.SetupHeader(i, headerCell);
                }

                sheetRow++;
            }

            foreach (var item in collection)
            {
                foreach (var columnMapping in mapping.Values)
                {
                    columnMapping.WriteCell(item, target.Cells[sheetRow, columnMapping.CellLocation.Column + 1 + startColumn], settings);
                }

                sheetRow++;
            }
        }
    }
}
