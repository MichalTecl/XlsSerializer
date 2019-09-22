using OfficeOpenXml;
using OfficeOpenXml.DataValidation.Contracts;
using XlsSerializer.Core.Attributes.Contract;

namespace XlsSerializer.Core.Attributes
{
    public class ListValidationAttribute : ValidationAttributeBase
    {
        private readonly string m_range;

        public ListValidationAttribute(string range)
        {
            m_range = range;
        }

        protected override IExcelDataValidation CreateValidation(ExcelRange cell)
        {
            var validation = cell.DataValidation.AddListDataValidation();
            validation.Formula.ExcelFormula = m_range;
            
            return validation;
        }
    }
}
