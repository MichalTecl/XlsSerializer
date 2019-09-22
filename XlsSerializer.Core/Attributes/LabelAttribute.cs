using System;
using System.Reflection;
using OfficeOpenXml;
using XlsSerializer.Core.Attributes.Contract;

namespace XlsSerializer.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class LabelAttribute : Attribute, ICanSetupCell
    {
        private readonly string m_text;
        private readonly LabelLocation m_location;

        public LabelAttribute(string text, LabelLocation location = LabelLocation.Left)
        {
            m_location = location;
            m_text = text;
        }

        public void Apply(PropertyInfo boundProperty, ExcelRange cell)
        {
            var rowOffset = GetLocationOffsets(m_location);

            var labelColumn = cell.Start.Column + rowOffset.Item1;
            var labelRow = cell.Start.Row + rowOffset.Item2;

            var labelCell = cell.Worksheet.Cells[labelRow, labelColumn];

            labelCell.Value = m_text;

            foreach (var setup in LabelStyleAttribute.FindStyle(boundProperty))
            {
                setup.Apply(boundProperty, labelCell);
            }
        }

        private static Tuple<int, int> GetLocationOffsets(LabelLocation location)
        {
            switch (location)
            {
                case LabelLocation.Left: return new Tuple<int, int>(-1, 0);
                case LabelLocation.Top: return new Tuple<int, int>(0, -1);
                default:
                    throw new ArgumentOutOfRangeException(nameof(location), location, null);
            }
        }
    }
}
