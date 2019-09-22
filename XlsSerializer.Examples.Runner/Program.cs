using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XlsSerializer.Examples.Core;
using XlsSerializer.Examples.Public.SimpleCollection;

namespace XlsSerializer.Examples.Runner
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var assembly = typeof(SimpleCollectionSerializationExample).Assembly;

            ExamplesLauncher.Launch(assembly, @"C:\Users\mtecl\Documents\GitHub\XlsSerializer\XlsSerializer\XlsSerializer.Examples.Public", "EXAMPLES");
        }
    }
}
