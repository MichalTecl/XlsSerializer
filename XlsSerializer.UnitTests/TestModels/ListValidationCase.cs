using System.Collections.Generic;
using OfficeOpenXml.DataValidation;
using XlsSerializer.Core.Attributes;

namespace XlsSerializer.UnitTests.TestModels
{
    public class ListValidationCase
    {
        [XlsSheet(1)]
        public MainSheet Main { get; } = new MainSheet();

        [XlsSheet(2, "Data")]
        public DataSheet Data { get; } = new DataSheet();
    }

    public class MainSheet
    {
        [XlsCell(0,0)]
        public string ItemTitle { get; } = "Validated";
        
        [ListValidation("Data!A1:A100", AllowBlank = false, Error = "ERROR", ErrorStyle = ExcelDataValidationWarningStyle.stop, ErrorTitle = "ERROR_TITLE", Prompt = "PROMPT", PromptTitle = "PROMPT_TITLE")]
        [XlsCell(1, 0)]
        public string Item { get; set; }
    }
    
    public class DataSheet
    {
        public DataSheet()
        {
            for (var i = 0; i < 50; i++)
            {
                ValidationItems.Add($"item_{i}");
            }
        }
        
        [XlsCell(0,0)]
        public List<string> ValidationItems { get; } = new List<string>();
    }
}
