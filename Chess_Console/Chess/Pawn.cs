using GameBoard;

namespace Chess
{
    class Pawn : Piece
    {
        public Pawn(Board board, Color color) : base(board, color)
        {
        }

        private bool CanCapture(Position pos)
        {
            Piece p = Board.GetPiece(pos);
            return p != null && p.Color != Color;
        }

        private bool CanMove(Position pos)
        {
            Piece p = Board.GetPiece(pos);
            return p == null;
        }

        public override bool[,] PossibleMoves()
        {
            bool[,] mat = new bool[Board.Rows, Board.Columns];

            Position pos = new Position(0, 0);
            
            if (Color == Color.White)
            {
                pos.DefinePosition(Position.Row - 1, Position.Column);
                if (Board.ValidPosition(pos) && CanMove(pos))
                {
                    mat[pos.Row, pos.Column] = true;
                }

                pos.DefinePosition(Position.Row - 2, Position.Column);
                if (Board.ValidPosition(pos) && CanMove(pos) && NumberOfMoves == 0)
                {
                    mat[pos.Row, pos.Column] = true;                    
                }

                pos.DefinePosition(Position.Row - 1, Position.Column + 1);
                if (Board.ValidPosition(pos) && CanCapture(pos))
                {
                    mat[pos.Row, pos.Column] = true;
                }

                pos.DefinePosition(Position.Row - 1, Position.Column - 1);
                if (Board.ValidPosition(pos) && CanCapture(pos))
                {
                    mat[pos.Row, pos.Column] = true;
                }
            }
            else
            {
                pos.DefinePosition(Position.Row + 1, Position.Column);
                if (Board.ValidPosition(pos) && CanMove(pos))
                {
                    mat[pos.Row, pos.Column] = true;
                }

                pos.DefinePosition(Position.Row + 2, Position.Column);
                if (Board.ValidPosition(pos) && CanMove(pos) && NumberOfMoves == 0)
                {
                    mat[pos.Row, pos.Column] = true;
                }

                pos.DefinePosition(Position.Row + 1, Position.Column + 1);
                if (Board.ValidPosition(pos) && CanCapture(pos))
                {
                    mat[pos.Row, pos.Column] = true;
                }

                pos.DefinePosition(Position.Row + 1, Position.Column - 1);
                if (Board.ValidPosition(pos) && CanCapture(pos))
                {
                    mat[pos.Row, pos.Column] = true;
                }
            }

            return mat;
        }

        public override string ToString()
        {
            return "P";
        }
    }
}