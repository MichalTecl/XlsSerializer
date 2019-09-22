using System;

namespace XlsSerializer.Core.Attributes
{
    [Flags]
    public enum FontStyle : int
    {
        Bold = 1,
        Italic = 2,
        Underline = 4,
        Strike = 8,
        Normal = 0
    }
}
