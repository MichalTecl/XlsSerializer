using System;
using System.Collections;
using System.IO;
using OfficeOpenXml;
using XlsSerializer.Core.Features;
using XlsSerializer.Core.Utils;

namespace XlsSerializer.Core
{
    public class XlsxSerializer : IXlsxSerializer
    {
        public static readonly IXlsxSerializer Instance = new XlsxSerializer(); 

        public void Serialize(object model, ExcelPackage package)
        {
            if (model == null)
            {
                return;
            }

            if (ReflectionHelper.GetIsCollection(model.GetType(), out var itemType, true))
            {
                XlsCollectionSerializerCore.SerializeCollection(itemType, (IEnumerable)model, XlsSheetSerializerCore.GetDefaultWorksheet(package), 0,0);
                return;
            }

            XlsWorkbookSerializerCore.SerializeWorkbook(model, package.Workbook);
        }

        public void Serialize(object model, string fileName)
        {
            using (var excelPackage = new ExcelPackage())
            {
                Serialize(model, excelPackage);

                excelPackage.SaveAs(new FileInfo(fileName));
            }
        }

        public void Serialize(object model, Stream stream)
        {
            using (var excelPackage = new ExcelPackage())
            {
                Serialize(model, excelPackage);

                excelPackage.SaveAs(stream);
            }
        }

        public void DeserializeTo(object model, ExcelPackage package)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            
            if (ReflectionHelper.GetIsCollection(model.GetType(), out var itemType, false))
            {
                var rawCollection = XlsCollectionDeserializerCore.DeserializeCollection(itemType,
                    XlsSheetSerializerCore.GetDefaultWorksheet(package), () => Activator.CreateInstance(itemType), 0,
                    0);

                ReflectionHelper.PopulateCollection(rawCollection, model);

                return;
            }

            XlsWorkbookDeserializerCore.DeserializeWorkbook(model, package.Workbook);
            XlsSheetDeserializerCore.Deserialize(model, XlsSheetSerializerCore.GetDefaultWorksheet(package));
        }

        public void DeserializeTo(object model, string fileName)
        {
            using (var package = new ExcelPackage(new FileInfo(fileName)))
            {
                DeserializeTo(model, package);
            }
        }

        public void DeserializeTo(object model, Stream stream)
        {
            using (var package = new ExcelPackage(stream))
            {
                DeserializeTo(model, package);
            }
        }

        public T Deserialize<T>(ExcelPackage package) where T : new()
        {
            var model = new T();
            DeserializeTo(model, package);
            return model;
        }

        public T Deserialize<T>(string fileName) where T : new()
        {
            using (var package = new ExcelPackage(new FileInfo(fileName)))
            {
                return Deserialize<T>(package);
            }
        }

        public T Deserialize<T>(Stream stream) where T : new()
        {
            using (var package = new ExcelPackage(stream))
            {
                return Deserialize<T>(package);
            }
        }

        public byte[] Serialize(object model)
        {
            using (var package = new ExcelPackage())
            {
                Serialize(model, package);

                return package.GetAsByteArray();
            }
        }
    }
}
