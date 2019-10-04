using System.IO;

using OfficeOpenXml;

namespace XlsSerializer.Core
{
    public interface IXlsxSerializer
    {
        void Serialize(object model, ExcelPackage package);

        void Serialize(object model, string fileName);

        void Serialize(object model, Stream stream);

        void DeserializeTo(object model, ExcelPackage package);

        void DeserializeTo(object model, string fileName);

        void DeserializeTo(object model, Stream stream);
        
        T Deserialize<T>(ExcelPackage package) where T : new();

        T Deserialize<T>(string fileName) where T : new();

        T Deserialize<T>(Stream stream) where T : new();

        byte[] Serialize(object model);
    }
}
