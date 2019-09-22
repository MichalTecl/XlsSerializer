using System;
using System.Reflection;
using OfficeOpenXml;
using OfficeOpenXml.DataValidation;
using OfficeOpenXml.DataValidation.Contracts;

namespace XlsSerializer.Core.Attributes.Contract
{
    public abstract class ValidationAttributeBase : Attribute, ICanSetupCell
    {

        /// <summary>Controls how Excel will handle invalid values.</summary>
        public ExcelDataValidationWarningStyle ErrorStyle { get; set; }

        /// <summary>True if input message should be shown</summary>
        public bool AllowBlank { get; set; }

        /// <summary>
        /// Title of error message box (see property ShowErrorMessage)
        /// </summary>
        public string ErrorTitle { get; set; }

        /// <summary>
        /// Error message box text (see property ShowErrorMessage)
        /// </summary>
        public string Error { get; set; }

        /// <summary>
        /// Title of info box if input message should be shown (see property ShowInputMessage)
        /// </summary>
        public string PromptTitle { get; set; }

        /// <summary>Info message text (see property ShowErrorMessage)</summary>
        public string Prompt { get; set; }
        
        protected abstract IExcelDataValidation CreateValidation(ExcelRange cell);

        public void Apply(PropertyInfo boundProperty, ExcelRange cell)
        {
            var validation = CreateValidation(cell);

            validation.ErrorStyle = ErrorStyle;
            validation.AllowBlank = AllowBlank;
            validation.ShowInputMessage = !string.IsNullOrWhiteSpace(Prompt) || !string.IsNullOrWhiteSpace(PromptTitle);
            validation.ShowErrorMessage = !string.IsNullOrWhiteSpace(ErrorTitle) || !string.IsNullOrWhiteSpace(Error);
            validation.ErrorTitle = ErrorTitle;
            validation.Error = Error;
            validation.PromptTitle = PromptTitle;
            validation.Prompt = Prompt;
        }
    }
}
