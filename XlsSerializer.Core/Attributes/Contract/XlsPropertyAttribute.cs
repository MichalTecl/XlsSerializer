using System;
using System.Linq;
using System.Reflection;
using OfficeOpenXml;
using XlsSerializer.Core.Features;
using XlsSerializer.Core.SettingsElements;
using XlsSerializer.Core.Utils;

namespace XlsSerializer.Core.Attributes.Contract
{
    public abstract class XlsPropertyAttribute : Attribute, ICellValueAccessor, ICanSetupCell, IHasProcessingOrder
    {
        protected XlsPropertyAttribute(string numberFormat)
        {
            NumberFormat = numberFormat;
        }

        public string NumberFormat { get; }

        public int ProcessingOrder => 0;

        public virtual void WriteCellValue(PropertyInfo sourceProperty, object owner, ExcelRange cell, XlsxSerializerSettings settings)
        {
            Func<PropertyAndOwnerInstance, object> defaultValueReader = (pao) =>
            {
                if (!pao.Property.CanRead || (!string.IsNullOrWhiteSpace(cell.Formula)) || (!string.IsNullOrWhiteSpace(cell.FormulaR1C1)))
                {
                    return null;
                }

                return pao.Property.GetValue(pao.PropertyOwner, null);
            };

            Action<Type, object, ExcelRange, IValueConverter> defaultWrite = (t, val, cel, con) => cell.Value = con.ToCellValue(t, val);

            settings.CellWriterInterceptor.Write(owner, sourceProperty, defaultValueReader, cell, defaultWrite, settings);
        }

        public void ReadCellValue(ExcelRange cell, object owner, PropertyInfo targetProperty, XlsxSerializerSettings settings)
        {
            if (ReflectionHelper.GetIsCollection(targetProperty.PropertyType, out var collectionItemType, true))
            {
                var collection = XlsCollectionDeserializerCore.DeserializeCollection(collectionItemType, cell.Worksheet,
                    () => Activator.CreateInstance(collectionItemType), cell.Start.Row - 1, cell.Start.Column - 1, settings).OfType<object>().ToList();

                ReflectionHelper.PopulateCollectionProperty(owner, targetProperty, collection);

                return;
            }

            var pao = new PropertyAndOwnerInstance(targetProperty, owner);

            Func<PropertyAndOwnerInstance, bool> defaultShouldProcessCellDecision = (p) => p.Property.CanWrite;

            Func<ExcelRange, Type, PropertyAndOwnerInstance, IValueConverter, object> defaultConvertCellValueToType = (cl, tp, p, converter) =>
            {
                var cellValue = cl.Value;
                if (cellValue == null)
                {
                    return null;
                }

                return converter.FromCellValue(tp, cellValue); 
            };

            Action<PropertyAndOwnerInstance, object> defaultPropertySetter = (p, v) => p.Property.SetValue(p.PropertyOwner, v);

            settings.CellReaderInterceptor.ReadCell(cell,
                owner,
                targetProperty,
                defaultShouldProcessCellDecision,
                defaultConvertCellValueToType,
                defaultPropertySetter,
                settings);
        }

        public void Apply(PropertyInfo boundProperty, ExcelRange cell)
        {
            if (!string.IsNullOrWhiteSpace(NumberFormat))
            {
                cell.Style.Numberformat.Format = NumberFormat;
            }
        }
    }
}
