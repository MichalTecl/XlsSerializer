using System;
using System.Reflection;
using OfficeOpenXml;
using XlsSerializer.Core.Attributes.Contract;

namespace XlsSerializer.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class CellStyleAttribute : CellStyleAttributeBase, ICanSetupCell
    {
        public void Apply(PropertyInfo boundProperty, ExcelRange cell)
        {
            Apply(cell);
        }
    }
}
