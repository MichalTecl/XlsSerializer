using System.Collections.Generic;

using XlsSerializer.Core.Attributes;

namespace XlsSerializer.Examples.Public.ListValidation
{
    //#start_publishing
    public class SheetWithListValidation
    {
        [XlsCell("A1")]
        [ListValidation("Data_Colors!A:A", AllowBlank = false, Error = "Choose a color", ErrorTitle = "Invalid value", Prompt = "Please choose a color", PromptTitle = "Your favourite color")]
        public string SelectedColor { get; set; }
        
        [XlsSheet(2, "Data_Colors",  IsHidden = true)]
        public List<string> Colors { get; } = new List<string>();
    }
}
