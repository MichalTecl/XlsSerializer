using System.Reflection;
using OfficeOpenXml;
using XlsSerializer.Core.Attributes.Contract;

namespace XlsSerializer.Core.Attributes
{
    public class HeaderStyleAttribute : CellStyleAttributeBase, ISetupHeaderCell
    {
        public void Apply(PropertyInfo boundProperty, ExcelRange cell)
        {
            Apply(cell);
        }
    }
}
