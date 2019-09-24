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
        void WriteCell(object propertyOwner, ExcelRange cell, XlsxSerializerSettings settings);
        object ReadCell(Func<object> propertyOwnerFactory, ExcelRange cell, XlsxSerializerSettings settings);

        void SetupHeader(ExcelRange headerCell);
    }
}