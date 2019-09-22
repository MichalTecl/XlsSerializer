using System.Reflection;
using OfficeOpenXml;

namespace XlsSerializer.Core.Attributes.Contract
{
    public interface ICellValueAccessor
    {
        void WriteCellValue(PropertyInfo sourceProperty, object owner, ExcelRange cell);

        void ReadCellValue(ExcelRange cell, object owner, PropertyInfo targetProperty);
    }
}
