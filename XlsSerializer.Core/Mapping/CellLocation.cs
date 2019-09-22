namespace XlsSerializer.Core.Mapping
{
    internal struct CellLocation
    {
        public readonly int? Row;
        public readonly int Column;

        public CellLocation(int? row, int column)
        {
            Row = row;
            Column = column;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is CellLocation))
            {
                return false;
            }

            return (((CellLocation) obj).Column == Column) && (((CellLocation) obj).Row == Row);
        }

        public override int GetHashCode()
        {
            return (Row ?? 0).GetHashCode() ^ Column.GetHashCode();
        }
    }
}
