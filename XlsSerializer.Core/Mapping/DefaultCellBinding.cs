﻿using System;
using System.Reflection;
using OfficeOpenXml;

namespace XlsSerializer.Core.Mapping
{
    internal class DefaultCellBinding : ICellBinding
    {
        public bool HasHeader { get; } = false;
        
        public CellLocation CellLocation { get; }
        public PropertyInfo BoundProperty { get; }
        public void WriteCell(object propertyOwner, ExcelRange cell)
        {
            cell.Value = propertyOwner;
        }

        public object ReadCell(Func<object> propertyOwner, ExcelRange cell)
        {
            return cell.Value;
        }

        public void SetupHeader(ExcelRange headerCell)
        {
        }
    }
}
