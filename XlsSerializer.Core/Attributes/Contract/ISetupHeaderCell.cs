using System.Reflection;
using OfficeOpenXml;

namespace XlsSerializer.Core.Attributes.Contract
{
    public interface ISetupHeaderCell
    {
        void Apply(PropertyInfo boundProperty, ExcelRange cell);
    }
}
