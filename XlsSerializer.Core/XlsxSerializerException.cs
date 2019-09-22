using System;
using OfficeOpenXml;

namespace XlsSerializer.Core
{
    public class XlsxSerializerException : Exception
    {
        internal XlsxSerializerException(ExcelRange range, Exception innerException) : base(
            $"Serialization or deserialization failed for cell [{range.Address}]: {innerException.Message}",
            innerException)
        {
        }
    }
}
