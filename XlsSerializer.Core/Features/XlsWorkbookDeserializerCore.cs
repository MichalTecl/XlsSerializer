using System;
using System.Linq;
using OfficeOpenXml;
using XlsSerializer.Core.Mapping;
using XlsSerializer.Core.Utils;

namespace XlsSerializer.Core.Features
{
    internal class XlsWorkbookDeserializerCore
    {
        public static bool DeserializeWorkbook(object model, ExcelWorkbook source, XlsxSerializerSettings settings)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var mainsheetProcessed = false;
            foreach (var sheetAssociation in SheetAssociation.GetSheetAssociations(model.GetType()))
            {
                var sheet = source.Worksheets[sheetAssociation.SheetName];
                if (sheet == null)
                {
                    continue;
                }

                if (sheetAssociation.BoundProperty == null)
                {
                    mainsheetProcessed = true;
                    XlsSheetDeserializerCore.Deserialize(model, sheet, settings);
                    continue;
                }

                if (ReflectionHelper.GetIsCollection(sheetAssociation.BoundProperty.PropertyType, out var itemType,
                    true))
                {
                    var collection = XlsCollectionDeserializerCore
                        .DeserializeCollection(itemType, sheet, () => Activator.CreateInstance(itemType), 0, 0, settings)
                        .OfType<object>().ToList();

                    ReflectionHelper.PopulateCollectionProperty(model, sheetAssociation.BoundProperty, collection);

                    continue;
                }

                var sheetModel = sheetAssociation.BoundProperty.GetValue(model, null);

                if (sheetModel == null)
                {
                    if (!sheetAssociation.BoundProperty.CanWrite)
                    {
                        continue;
                    }

                    sheetModel = Activator.CreateInstance(sheetAssociation.BoundProperty.PropertyType);
                    sheetAssociation.BoundProperty.SetValue(model, sheetModel, null);
                }

                XlsSheetDeserializerCore.Deserialize(sheetModel, sheet, settings);
            }

            return mainsheetProcessed;
        }
    }
}
