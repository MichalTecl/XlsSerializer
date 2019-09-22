using System;
using System.Reflection;
using OfficeOpenXml;
using XlsSerializer.Core.Attributes.Contract;

namespace XlsSerializer.Core.Attributes
{
    public class FormulaAttribute : Attribute, ICanSetupCell
    {
        private readonly string m_formula;

        public FormulaAttribute(string formula)
        {
            formula = formula.Trim();
            if (!formula.StartsWith("="))
            {
                formula = $"={formula}";
            }

            m_formula = formula;
        }

        public void Apply(PropertyInfo boundProperty, ExcelRange cell)
        {
            cell.Formula = m_formula;
        }
    }
}
