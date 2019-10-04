using System;
using System.Collections.Generic;
using System.Linq;

using XlsSerializer.Examples.Core;
using XlsSerializer.Examples.Public.CellLabels;

using Xunit;

namespace XlsSerializer.UnitTests
{
    public class RunExamplesAsTests
    {
        public static IEnumerable<object[]> TestCases
        {
            get
            {
                foreach (var type in typeof(CellLabelsExample).Assembly.GetTypes().Where(t => typeof(ExampleTestBase).IsAssignableFrom(t)))
                {
                    yield return new object[] { type.Name, type };
                }
            }
        }

        [Theory]
        [MemberData(nameof(TestCases))]
        public void RunExamples(string name, Type type)
        {
            var obj = (ExampleTestBase)Activator.CreateInstance(type);

            obj.Run();
        }
    }
}
