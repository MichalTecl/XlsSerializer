using System;
using System.Linq;
using OfficeOpenXml;

namespace XlsSerializer.Core.Features
{
    internal static class XlsSheetSerializerCore
    {
        public const string DefaultSheetName = "Sheet1";

        public static void Serialize(Type modelType, object model, ExcelWorksheet target, XlsxSerializerSettings settings)
        {
            var mapping = DocumentMapper.GetMapping(modelType);

            foreach (var m in mapping.Values)
            {
                if (m.CellLocation.Row == null)
                {
                    continue;
                }

                var cell = target.Cells[m.CellLocation.Row.Value + 1, m.CellLocation.Column + 1];

                m.WriteCell(model, cell, settings);
            }
        }

        internal static ExcelWorksheet GetDefaultWorksheet(ExcelPackage package)
        {
            return package.Workbook.Worksheets.FirstOrDefault(s => s.Name.Equals(DefaultSheetName, StringComparison.InvariantCultureIgnoreCase)) ?? package.Workbook.Worksheets.Add(DefaultSheetName);
        }
    }
}
