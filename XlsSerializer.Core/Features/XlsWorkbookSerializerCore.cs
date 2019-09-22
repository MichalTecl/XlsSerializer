using System.Collections;
using OfficeOpenXml;
using XlsSerializer.Core.Mapping;
using XlsSerializer.Core.Utils;

namespace XlsSerializer.Core.Features
{
    internal static class XlsWorkbookSerializerCore
    {
        public static void SerializeWorkbook(object model, ExcelWorkbook target)
        {
            if (model == null)
            {
                return;
            }
            
            foreach (var sheetAssociation in SheetAssociation.GetSheetAssociations(model.GetType()))
            {
                var sheet = target.Worksheets[sheetAssociation.SheetName];

                if (sheet == null)
                {
                    sheet = target.Worksheets.Add(sheetAssociation.SheetName);

                    foreach (var setup in sheetAssociation.SheetSetup)
                    {
                        setup.SetupSheet(sheet);
                    }
                }
                            

                object sheetModelValue;

                if (sheetAssociation.BoundProperty == null) 
                {
                    sheetModelValue = model;
                    XlsSheetSerializerCore.Serialize(model.GetType(), model, sheet);
                    continue;
                }

                if ((!sheetAssociation.BoundProperty.CanRead) ||
                    ((sheetModelValue = sheetAssociation.BoundProperty.GetValue(model)) == null))
                {
                    continue;
                }

                if (ReflectionHelper.GetIsCollection(sheetAssociation.BoundProperty.PropertyType,
                    out var collectionItemType, true))
                {
                    XlsCollectionSerializerCore.SerializeCollection(collectionItemType, (IEnumerable)sheetModelValue, sheet, 0,0);
                }
                else
                {
                    XlsSheetSerializerCore.Serialize(sheetAssociation.BoundProperty.PropertyType, sheetModelValue, sheet);
                }
            }
        }
    }
}
