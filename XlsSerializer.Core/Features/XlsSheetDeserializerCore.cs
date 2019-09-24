using System;
using OfficeOpenXml;

namespace XlsSerializer.Core.Features
{
    internal static class XlsSheetDeserializerCore
    {
        public static void Deserialize(object modelInstance, ExcelWorksheet source, XlsxSerializerSettings settings)
        {
            if (modelInstance == null)
            {
                throw new ArgumentNullException(nameof(modelInstance));
            }

            var mapping = DocumentMapper.GetMapping(modelInstance.GetType());

            foreach (var rule in mapping.Values)
            {
                if (rule.CellLocation.Row == null)
                {
                    continue;
                }

                var sourceCell = source.Cells[rule.CellLocation.Row.Value + 1, rule.CellLocation.Column + 1];
                rule.ReadCell(() => modelInstance, sourceCell, settings);
            }
        }
    }
}
