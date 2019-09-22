using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using XlsSerializer.Examples.Core.Model;

namespace XlsSerializer.Examples.Core
{
    public static class ExamplesLauncher
    {
        public static void Launch(Assembly assembly, string projectRootPath, string targetDirectory)
        {
            var projectFiles = Directory.EnumerateFiles(projectRootPath, "*.cs", SearchOption.AllDirectories)
                .Select(f => new FileInfo(f)).ToList();

            var testClasses = assembly.GetTypes()
                .Where(type => type.IsClass && !type.IsAbstract && typeof(ExampleTestBase).IsAssignableFrom(type))
                .Select(ExampleTestAttribute.Find)
                .OrderBy(a => a.Item2.Order).ToList();
            
            var examples = new List<ExampleModel>(testClasses.Count);

            foreach (var testType in testClasses)
            {
                var data = RunTest(testType.Item1, testType.Item2, projectFiles, targetDirectory);
                if (data != null)
                {
                    examples.Add(data);
                }
            }

            SaveExamples(examples, targetDirectory);
        }

        private static void SaveExamples(List<ExampleModel> examples, string targetDirectory)
        {
            var outputsDirectory = Path.Combine(targetDirectory, "Xlsx");
            Directory.CreateDirectory(targetDirectory);
            Directory.CreateDirectory(outputsDirectory);
            
            var index = new List<IndexItem>();

            foreach (var i in examples)
            {
                index.Add(new IndexItem
                {
                    Key = i.Key,
                    Title = i.Title
                });

                File.WriteAllBytes(Path.Combine(outputsDirectory, i.OutputFile), i.OutputFileContent);
                File.WriteAllText(Path.Combine(targetDirectory, $"{i.Key}.json"), JsonConvert.SerializeObject(i));
            }

            File.WriteAllText(Path.Combine(targetDirectory, "index.json"), JsonConvert.SerializeObject(index));
        }

        private static ExampleModel RunTest(Type testClass, ExampleTestAttribute testAttribute, List<FileInfo> projectFiles, string targetDirectory)
        {
            var testObj = (ExampleTestBase)Activator.CreateInstance(testClass);

            var exampleFile = projectFiles.FirstOrDefault(f =>
                f.Name.Equals($"{testClass.Name}.cs", StringComparison.InvariantCultureIgnoreCase));

            if (exampleFile == null)
            {
                throw new InvalidOperationException("Example file not found");
            }

            var xlsx = testObj.Run();

            var key = GetKey(testAttribute.Title);

            var result = new ExampleModel();
            result.OutputFileContent = xlsx;
            result.OutputFile = $"{key}.xlsx";
            result.Title = testAttribute.Title;
            result.Key = key;

            ReadSourceFiles(exampleFile, result.Sources);

            return result;
        }

        private static void ReadSourceFiles(FileInfo main, List<ExampleSourceFileModel> target)
        {
            target.AddRange(Directory.GetFiles(main.DirectoryName, "*.cs", SearchOption.AllDirectories).Select(fi => new ExampleSourceFileModel
            {
                Content = ReadCsFile(fi),
                Name = Path.GetFileName(fi),
                IsDefault = main.FullName.Equals(fi, StringComparison.InvariantCultureIgnoreCase)
            }).OrderBy(f => f.Name));
        }

        private static string ReadCsFile(string fi)
        {
            var fileText = File.ReadAllText(fi);

            return fileText;
        }

        private static string GetKey(string title)
        {
            return string.Join(string.Empty, title.Where(char.IsLetter));
        }
    }
}
