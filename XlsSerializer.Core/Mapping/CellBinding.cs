using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using OfficeOpenXml;
using XlsSerializer.Core.Attributes.Contract;
using XlsSerializer.Core.Utils;

namespace XlsSerializer.Core.Mapping
{
    internal sealed class CellBinding : ICellBinding
    {
        private readonly List<ICanSetupCell> m_cellSetups = new List<ICanSetupCell>();
        private readonly List<ISetupHeaderCell> m_headerSetups = new List<ISetupHeaderCell>();
        private readonly List<ICellValueAccessor> m_cellValueAccessors = new List<ICellValueAccessor>();
        
        private CellBinding(CellLocation cellLocation, PropertyInfo boundProperty, Attribute[] attributes)
        {
            BoundProperty = boundProperty;
            CellLocation = cellLocation;

            m_cellSetups.AddRange(attributes.OfType<ICanSetupCell>());
            m_cellValueAccessors.AddRange(attributes.OfType<ICellValueAccessor>());
            m_headerSetups.AddRange(attributes.OfType<ISetupHeaderCell>());

            var headerText = attributes.OfType<IHasHeader>().LastOrDefault()?.HeaderText;

            if(headerText != null)
            {
                m_headerSetups.Add(new HeaderTextSetup(headerText));
            }
        }

        public bool HasHeader => m_headerSetups.Any();

        public CellLocation CellLocation { get; }
        
        public PropertyInfo BoundProperty { get; }

        public static IEnumerable<ICellBinding> CreateMap(Type t)
        {
            var classAttributes = t.GetCustomAttributes().OrderBy(a => a.GetProcessingOrder()).ToArray();

            var result = new List<ICellBinding>();

            var hasSheet = false;

            foreach (var property in t.GetProperties())
            {
                var attributes = property.GetCustomAttributes(true).OfType<Attribute>()
                    .OrderBy(a => a.GetProcessingOrder()).ToArray();

                attributes = JoinAttributes(classAttributes, attributes);

                hasSheet = attributes.OfType<IHasSheet>().Any();

                var columnIndex = attributes.OfType<IHasColumnIndex>().LastOrDefault();
                if (columnIndex == null)
                {
                    continue;
                }

                var rowIndex = attributes.OfType<IHasRowIndex>().LastOrDefault()?.RowIndex;

                var location = new CellLocation(rowIndex, columnIndex.ColumnIndex);

                result.Add(new CellBinding(location, property, attributes)); 
            }

            if (!result.Any() && !hasSheet)
            {
                result.Add(new DefaultCellBinding());
            }

            return result;
        }

        public void WriteCell(object propertyOwner, ExcelRange cell, XlsxSerializerSettings settings)
        {
            foreach (var cellSetup in m_cellSetups)
            {
                cellSetup.Apply(BoundProperty, cell);
            }
            
            foreach (var setter in m_cellValueAccessors)
            {
                setter.WriteCellValue(BoundProperty, propertyOwner, cell, settings);
            }
        }

        public object ReadCell(Func<object> propertyOwnerFactory, ExcelRange cell, XlsxSerializerSettings settings)
        {
            if (cell.Value == null)
            {
                return null;
            }

            var owner = propertyOwnerFactory();

            foreach (var setter in m_cellValueAccessors)
            {
                setter.ReadCellValue(cell, owner, BoundProperty, settings);
            }

            return owner;
        }

        public void SetupHeader(ExcelRange headerCell)
        {
            foreach (var s in m_headerSetups)
            {
                s.Apply(BoundProperty, headerCell);
            }
        }

        private static Attribute[] JoinAttributes(Attribute[] classAttributes, Attribute[] propertyAttributes)
        {
            var res = new Attribute[classAttributes.Length + propertyAttributes.Length];

            var ci = 0;
            for (; ci < classAttributes.Length; ci++)
            {
                res[ci] = classAttributes[ci];
            }

            foreach (var propertyAttribute in propertyAttributes)
            {
                res[ci] = propertyAttribute;
                ci++;
            }

            return res;
        }
        
        private sealed class HeaderTextSetup : ISetupHeaderCell
        {
            private readonly string m_headerText;

            public HeaderTextSetup(string headerText)
            {
                m_headerText = headerText;
            }

            public void Apply(PropertyInfo boundProperty, ExcelRange cell)
            {
                cell.Value = m_headerText;
            }
        }
    }
}
