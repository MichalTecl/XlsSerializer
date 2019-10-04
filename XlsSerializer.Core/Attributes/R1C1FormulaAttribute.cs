using System;
using System.Reflection;
using OfficeOpenXml;
using XlsSerializer.Core.Attributes.Contract;

namespace XlsSerializer.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class R1C1FormulaAttribute : Attribute, ICanSetupCell
    {
        private readonly string m_formula;

        public R1C1FormulaAttribute(string formula)
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
            cell.FormulaR1C1 = m_formula;
        }
    }
}
