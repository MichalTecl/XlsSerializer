using System;
using System.Reflection;
using OfficeOpenXml;

namespace XlsSerializer.Core.Mapping
{
    internal interface ICellBinding
    {
        bool HasHeader { get; }
        CellLocation CellLocation { get; }
        PropertyInfo BoundProperty { get; }
        void WriteCell(object propertyOwner, ExcelRange cell);
        object ReadCell(Func<object> propertyOwnerFactory, ExcelRange cell);

        void SetupHeader(ExcelRange headerCell);
    }
}