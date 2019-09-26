using System;
using System.Collections.Generic;
using System.IO;
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

            var cdir = Directory.GetCurrentDirectory(); //C:\Users\mtecl\Documents\GitHub\XlsSerializer\XlsSerializer\XlsSerializer.Examples.Runner\bin\Debug

            ExamplesLauncher.Launch(assembly, 
                Path.Combine(cdir, @"..\..\..\XlsSerializer.Examples.Public"), 
                Path.Combine(cdir, @"..\..\..\XlsSerializer.Examples.Web\data"));
        }
    }
}
