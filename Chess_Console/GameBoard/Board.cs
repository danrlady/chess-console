namespace GameBoard
{
    class Board
    {
        public int Rows { get; set; }
        public int Columns { get; set; }
        private Piece[,] Pieces;

        public Board(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            Pieces = new Piece[Rows, Columns];
        }

        public Piece GetPiece(int row, int column)
        {
            return Pieces[row, column];
        }

        public Piece GetPiece(Position pos)
        {
            return Pieces[pos.Row, pos.Column];
        }

        public void PutPiece(Piece p, Position pos)
        {
            if (OccupiedPosition(pos))
            {
                throw new BoardException("There's already a piece in this position.");
            }
            Pieces[pos.Row, pos.Column] = p;
            p.Position = pos;
        }

        public Piece RemovePiece(Position pos)
        {
            if(GetPiece(pos) == null)
            {
                return null;
            }
            Piece aux = GetPiece(pos);
            aux.Position = null;
            Pieces[pos.Row, pos.Column] = null;
            return aux;
        }
        
        public bool ValidPosition(Position pos)
        {
            if (pos.Row < 0 || pos.Row > Rows || pos.Column < 0 || pos.Column > Columns)
            {
                return false;
            }

            return true;
        }

        public void ValidatePosition(Position pos)
        {
            if (!ValidPosition(pos))
            {
                throw new BoardException("Invalid position!");
            }
        }

        public bool OccupiedPosition(Position pos)
        {
            ValidatePosition(pos);
            return GetPiece(pos) != null;
        }
    }
}
