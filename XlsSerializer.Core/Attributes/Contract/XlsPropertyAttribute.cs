using System;
using System.Linq;
using System.Reflection;
using OfficeOpenXml;
using XlsSerializer.Core.Features;
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

        public virtual void WriteCellValue(PropertyInfo sourceProperty, object owner, ExcelRange cell)
        {
            if (!sourceProperty.CanRead || (!string.IsNullOrWhiteSpace(cell.Formula)) || (!string.IsNullOrWhiteSpace(cell.FormulaR1C1)))
            {
                return;
            }
            
            var value = sourceProperty.GetValue(owner, null);
            if (value == null)
            {
                return;
            }

            cell.Value = value;
        }

        public void ReadCellValue(ExcelRange cell, object owner, PropertyInfo targetProperty)
        {
            
            if (ReflectionHelper.GetIsCollection(targetProperty.PropertyType, out var collectionItemType, true))
            {
                var collection = XlsCollectionDeserializerCore.DeserializeCollection(collectionItemType, cell.Worksheet,
                    () => Activator.CreateInstance(collectionItemType), cell.Start.Row - 1, cell.Start.Column - 1).OfType<object>().ToList();

                ReflectionHelper.PopulateCollectionProperty(owner, targetProperty, collection);

                return;
            }

            if (!targetProperty.CanWrite)
            {
                return;
            }

            var cellValue = cell.Value;

            if (cellValue == null)
            {
                return;
            }

            var convertedValue = Convert.ChangeType(cellValue, TryGetUnderlyingType(targetProperty.PropertyType));

            targetProperty.SetValue(owner, convertedValue, null);
        }

        public void Apply(PropertyInfo boundProperty, ExcelRange cell)
        {
            if (!string.IsNullOrWhiteSpace(NumberFormat))
            {
                cell.Style.Numberformat.Format = NumberFormat;
            }
        }

        private static Type TryGetUnderlyingType(Type type)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                return Nullable.GetUnderlyingType(type);
            }

            return type;
        }
    }
}
