using GameBoard;

namespace Chess
{
    class Pawn : Piece
    {
        public Pawn(Board board, Color color) : base(board, color)
        {
        }

        private bool CanMove(Position pos)
        {
            Piece p = Board.GetPiece(pos);
            return p != null && p.Color != Color;
        }

        public override bool[,] PossibleMoves()
        {
            bool[,] mat = new bool[Board.Rows, Board.Columns];

            Position pos = new Position(0, 0);

            //North
            pos.DefinePosition(Position.Row - 1, Position.Column);
            if (Color == Color.White && Board.ValidPosition(pos) && Board.GetPiece(pos) == null)
            {
                mat[pos.Row, pos.Column] = true;
                if (NumberOfMoves == 0)
                {
                    pos.Row--;
                    if (Board.GetPiece(pos) == null)
                    {
                        mat[pos.Row, pos.Column] = true;
                    }
                }
            }

            //NE
            pos.DefinePosition(Position.Row - 1, Position.Column + 1);
            if (Color == Color.White && Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }

            //NW
            pos.DefinePosition(Position.Row - 1, Position.Column - 1);
            if (Color == Color.White && Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }

            //South
            pos.DefinePosition(Position.Row + 1, Position.Column);
            if (Color == Color.Black && Board.ValidPosition(pos) && Board.GetPiece(pos) == null)
            {
                mat[pos.Row, pos.Column] = true;
                if (NumberOfMoves == 0)
                {
                    pos.Row++;
                    if (Board.GetPiece(pos) == null)
                    {
                        mat[pos.Row, pos.Column] = true;
                    }
                }
            }

            //SE
            pos.DefinePosition(Position.Row + 1, Position.Column + 1);
            if (Color == Color.Black && Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }

            //SW
            pos.DefinePosition(Position.Row + 1, Position.Column - 1);
            if (Color == Color.Black && Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }

            return mat;
        }

        public override string ToString()
        {
            return "P";
        }
    }
}