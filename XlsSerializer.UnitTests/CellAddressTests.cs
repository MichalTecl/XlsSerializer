using XlsSerializer.Core.Utils;
using Xunit;

namespace XlsSerializer.UnitTests
{
    public class CellAddressTests
    {
        [Theory]
        [InlineData("A", 0)]
        [InlineData("b", 1)]
        [InlineData("Z", 25)]
        [InlineData("AA", 26)]
        [InlineData("ZZ", 701)]
        [InlineData("AAA", 702)]
        public void TestColumnSymbolParsing(string symbol, int expected)
        {
            Assert.Equal(expected, CellAddress.ColumnSymbolToIndex(symbol));
        }

        [Theory]
        [InlineData("sheet1!A1:ZZ99", "sheet1", 0, 0, 701, 98, true)]
        [InlineData("'sheet1'!A1:ZZ99", "sheet1", 0, 0, 701, 98, true)]
        [InlineData("sheet1!A:Z", "sheet1", 0, null, 25, null, true)]
        [InlineData("A", null, 0, null, 0, null, false)]
        [InlineData("98", null, null, null, null, null, false)]
        [InlineData("B2", null, 1, 1, 1, 1, false)]
        public void TestAddressParsing(string address, string sheet, int? scol, int? srow, int? ecol, int? erow, bool isRange)
        {
            var result = CellAddress.Parse(address);

            Assert.Equal(sheet, result.Sheet);
            Assert.Equal(scol, result.StartColumn);
            Assert.Equal(srow, result.StartRow);
            Assert.Equal(ecol, result.EndColumn);
            Assert.Equal(erow, result.EndRow);
            Assert.Equal(isRange, result.IsRange);
        }
    }
}
