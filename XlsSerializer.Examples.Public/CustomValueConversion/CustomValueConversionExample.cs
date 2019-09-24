using System.Collections.Generic;
using System.IO;

using XlsSerializer.Core;
using XlsSerializer.Examples.Core;

using Xunit;

namespace XlsSerializer.Examples.Public.CustomValueConversion
{
    [ExampleTest(5001, "Custom Value Conversion")]
    public class CustomValueConversionExample : ExampleTestBase
    {
        protected override void Test(Stream target)
        {
            var model = new List<QuestionSheetItem>
            {
                new QuestionSheetItem
                {
                    Question = "Does XlsSerializer support custom value converters?",
                    Answer = true
                },
                new QuestionSheetItem
                {
                    Question = "Is it complicated?",
                    Answer = false
                },
                new QuestionSheetItem
                {
                    Question = "Is there any comparable library?",
                    Answer = null
                }
            };

            var serializer = new XlsxSerializer(new XlsxSerializerSettings(new CustomBoolTypeConverter()));

            serializer.Serialize(model, target);

            var deserialized = serializer.Deserialize<List<QuestionSheetItem>>(target);

            Assert.Equal(model, deserialized, CompareItems<QuestionSheetItem>((a,b) =>
            {
                Assert.Equal(a.Question, b.Question);
                Assert.Equal(a.Answer, b.Answer);

                return true;
            }));
        }
    }
}
