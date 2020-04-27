using GameBoard;

namespace Chess
{
    class ChessPosition
    {
        public char Row { get; set; }
        public int Column { get; set; }

        public ChessPosition(char row, int column)
        {
            Row = row;
            Column = column;
        }

        public Position ToPosition()
        {
            return new Position(8 - Row, Column - 'a');
        }

        public override string ToString()
        {
            return "" + Column + Row;
        }
    }
}
