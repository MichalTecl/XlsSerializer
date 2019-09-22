using System.Reflection;
using OfficeOpenXml;

namespace XlsSerializer.Core.Attributes.Contract
{
    public interface ISetupLabelCell
    {
        void Apply(PropertyInfo boundProperty, ExcelRange cell);
    }
}
