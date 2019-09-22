using System;
using System.Collections;
using OfficeOpenXml;

namespace XlsSerializer.Core.Features
{
    internal static class XlsCollectionDeserializerCore
    {
        public static IEnumerable DeserializeCollection(Type collectionItemType, ExcelWorksheet source, Func<object> newItemFactory, int startRow, int startColumn)
        {
            var mapping = DocumentMapper.GetMapping(collectionItemType);

            var sheetRow = (mapping.HasHeader ? 2 : 1) + startRow;

            for (; sheetRow <= source.Dimension.End.Row; sheetRow++)
            {
                object item = null;

                foreach (var cellMapping in mapping.Values)
                {
                    var cell = source.Cells[sheetRow, cellMapping.CellLocation.Column + 1 + startColumn];
                    try
                    {
                        var readingResult = cellMapping.ReadCell(item == null ? newItemFactory : () => item, cell);

                        if ((item == null) || (readingResult != null))
                        {
                            item = readingResult;
                        }

                        if (item is IHasSourceRowIndex rowReference)
                        {
                            rowReference.SourceRowIndex = sheetRow;
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new XlsxSerializerException(cell, ex);
                    }
                }

                if (item == null)
                {
                    yield break;
                }
                
                yield return item;
            }
        }
    }
}
