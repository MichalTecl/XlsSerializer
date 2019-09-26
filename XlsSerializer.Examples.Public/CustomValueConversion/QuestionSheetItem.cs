using XlsSerializer.Core.Attributes;

namespace XlsSerializer.Examples.Public.CustomValueConversion
{
    //#start_publishing
    public class QuestionSheetItem
    {
        [XlsColumn("A")]
        public string Question { get; set; }

        [XlsColumn("B")]
        public bool? Answer { get; set; }
    }
}
