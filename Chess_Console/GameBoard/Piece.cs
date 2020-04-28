namespace GameBoard
{
    abstract class Piece
    {
        public Position Position { get; set; }
        public Color Color { get; protected set; }
        public int NumberOfMoves { get; protected set; }
        public Board Board { get; protected set; }

        public Piece(Board board, Color color)
        {
            Board = board;
            Color = color;
            Position = null;
            NumberOfMoves = 0;
        }

        public void IncrementNumberOfMoves()
        {
            NumberOfMoves++;
        }

        public bool HasPossibleMoves()
        {
            bool[,] mat = PossibleMoves();
            for (int i = 0; i < Board.Rows; i++)
            {
                for (int j = 0; j < Board.Columns; j++)
                {
                    if (mat[i, j])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool CanMoveTo(Position pos)
        {
            return PossibleMoves()[pos.Row, pos.Column];
        }

        public abstract bool[,] PossibleMoves();

    }
}
