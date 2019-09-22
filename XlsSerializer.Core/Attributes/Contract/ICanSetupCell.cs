using System.Reflection;
using OfficeOpenXml;

namespace XlsSerializer.Core.Attributes.Contract
{
    public interface ICanSetupCell
    {
        void Apply(PropertyInfo boundProperty, ExcelRange cell);
    }
}
