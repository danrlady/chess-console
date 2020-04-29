using GameBoard;

namespace Chess
{
    class King : Piece
    {
        private ChessMatch _match;

        public King(Board board, Color color, ChessMatch match) : base(board, color)
        {
            _match = match;
        }

        private bool CanMove(Position pos)
        {
            Piece p = Board.GetPiece(pos);
            return p == null || p.Color != Color;
        }

        private bool TowerForCastling(Position pos)
        {
            Piece p = Board.GetPiece(pos);
            return p != null
                && p is Tower
                && p.Color == Color
                && p.NumberOfMoves == 0;
        }

        public override bool[,] PossibleMoves()
        {
            bool[,] mat = new bool[Board.Rows, Board.Columns];

            Position pos = new Position(0, 0);

            //North
            pos.DefinePosition(Position.Row - 1, Position.Column);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }

            //NE
            pos.DefinePosition(Position.Row - 1, Position.Column + 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }

            //East
            pos.DefinePosition(Position.Row, Position.Column + 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }

            //SE
            pos.DefinePosition(Position.Row + 1, Position.Column + 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }

            //South
            pos.DefinePosition(Position.Row + 1, Position.Column);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }

            //SW
            pos.DefinePosition(Position.Row + 1, Position.Column - 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }

            //West
            pos.DefinePosition(Position.Row, Position.Column - 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }

            //NW
            pos.DefinePosition(Position.Row - 1, Position.Column - 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }

            //Special Move           
            if (NumberOfMoves == 0 && !_match.Check)
            {
                //King side castling
                Position posT1 = new Position(Position.Row, Position.Column + 3);
                if (TowerForCastling(posT1))
                {
                    Position p1 = new Position(Position.Row, Position.Column + 1);
                    Position p2 = new Position(Position.Row, Position.Column + 2);
                    if (Board.GetPiece(p1) == null && Board.GetPiece(p2) == null)
                    {
                        mat[p2.Row, p2.Column] = true;
                    }
                }

                //Queen side castling
                Position posT2 = new Position(Position.Row, Position.Column + 3);
                if (TowerForCastling(posT2))
                {
                    Position p1 = new Position(Position.Row, Position.Column - 1);
                    Position p2 = new Position(Position.Row, Position.Column - 2);
                    Position p3 = new Position(Position.Row, Position.Column - 3);
                    if (Board.GetPiece(p1) == null && Board.GetPiece(p2) == null)
                    {
                        mat[p2.Row, p2.Column] = true;
                    }
                }
            }            

            return mat;
        }

        public override string ToString()
        {
            return "K";
        }
    }
}
