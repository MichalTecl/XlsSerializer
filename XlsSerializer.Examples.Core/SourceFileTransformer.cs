using System;
using System.Collections.Generic;
using System.Linq;

namespace XlsSerializer.Examples.Core
{
    public static class SourceFileTransformer
    {
        private const string c_startPublishingTag = "//#start_publishing";

        public static string Transform(string code)
        {
            var codeLines = code.Split(new string[] {"\r\n", "\r", "\n"}, StringSplitOptions.None);

            var startPublishingLine = codeLines.FirstOrDefault(l => l.Trim().Equals(c_startPublishingTag));

            if (startPublishingLine == null)
            {
                return code;
            }

            var baseIndent = 0;
            var lineIndex = 0;
            for (; lineIndex < codeLines.Length; lineIndex++)
            {
                if (codeLines[lineIndex] == startPublishingLine)
                {
                    baseIndent = startPublishingLine.Length - startPublishingLine.TrimStart().Length;
                    lineIndex++;
                    break;
                }
            }

            var result = new List<string>(codeLines.Length);

            for (; lineIndex < codeLines.Length; lineIndex++)
            {
                var line = codeLines[lineIndex];

                var lineIndent = line.Length - line.TrimStart().Length;
                if ((lineIndent < baseIndent) && (!string.IsNullOrWhiteSpace(line)))
                {
                    continue;
                }

                line = $"{string.Join(string.Empty, GetIndent(lineIndent - baseIndent))}{line.TrimStart()}";

                line = line.Replace(">", "&gt;").Replace("<", "&lt;");

                result.Add(line);
            }

            return string.Join(Environment.NewLine, result).Trim();
        }

        private static IEnumerable<char> GetIndent(int indent)
        {
            for (var i = 0; i < indent; i++)
            {
                yield return ' ';
            }
        }
    }
}
