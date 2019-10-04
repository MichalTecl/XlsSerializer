using System;
using System.IO;
using System.Linq;
using OfficeOpenXml;

namespace XlsSerializer.UnitTests.TestUtils
{
    internal static class ExcelPackageSaveAndLoad
    {
        public static void SaveAndLoadSheet(Action<ExcelWorksheet> write, Action<ExcelWorksheet> read, string outputFileName = null)
        {
           WorkbookTest(wb => write(wb.Worksheets.Add(Guid.NewGuid().ToString())), wb => read(wb.Worksheets.Single()), outputFileName);
        }

        public static void WorkbookTest(Action<ExcelWorkbook> toWrite, Action<ExcelWorkbook> loaded,
            string outputFileName = null)
        {
            using (var stream = new MemoryStream())
            {
                using (var packageToWrite = new ExcelPackage())
                {
                    toWrite(packageToWrite.Workbook);

                    packageToWrite.SaveAs(stream);

                    if (!string.IsNullOrWhiteSpace(outputFileName))
                    {
                        packageToWrite.SaveAs(new FileInfo(outputFileName));
                    }
                }

                stream.Position = 0;

                using (var packageToRead = new ExcelPackage(stream))
                {
                    loaded(packageToRead.Workbook);
                }
            }
        }
    }
}
