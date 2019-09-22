using System.Collections.Generic;
using Newtonsoft.Json;

namespace XlsSerializer.Examples.Core.Model
{
    public class ExampleModel
    {
        public string Title { get; set; }

        public string Key { get; set; }

        public List<ExampleSourceFileModel> Sources { get; } = new List<ExampleSourceFileModel>();

        public string OutputFile { get; set; }

        public string OutputFileLink { get; set; }

        [JsonIgnore]
        internal byte[] OutputFileContent { get; set; }
    }
}
