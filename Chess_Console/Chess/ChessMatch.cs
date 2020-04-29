using System.Collections.Generic;
using GameBoard;

namespace Chess
{
    class ChessMatch
    {
        public Board Board { get; private set; }
        public int Turn { get; private set; }
        public Color ActualPlayer { get; private set; }
        public bool Finished { get; private set; }
        private HashSet<Piece> Pieces;
        private HashSet<Piece> Captured;
        public bool Check { get; private set; }

        public ChessMatch()
        {
            Board = new Board(8, 8);
            Finished = false;
            Turn = 1;
            ActualPlayer = Color.White;
            Pieces = new HashSet<Piece>();
            Captured = new HashSet<Piece>();
            PutPieces();
        }
        
        private void ChangePlayer()
        {
            ActualPlayer = ActualPlayer == Color.White ? Color.Black : Color.White;
        }

        public void MakeAMove(Position origin, Position destination)
        {
            Piece capturedPiece = MovePiece(origin, destination);

            if (IsInCheck(ActualPlayer))
            {
                UndoMove(origin, destination, capturedPiece);
                throw new BoardException("You can't put yourself in check.");
            }

            Check = IsInCheck(GetEnemyColor(ActualPlayer)) ? true : false;

            if (TestCheckMate(GetEnemyColor(ActualPlayer)))
            {
                Finished = true;
            }
            else
            {
                Turn++;
                ChangePlayer();
            }
        }

        public Piece MovePiece(Position origin, Position destination)
        {
            Piece p = Board.RemovePiece(origin);
            p.IncreaseNumberOfMoves();
            Piece capturedPiece = Board.RemovePiece(destination);
            Board.PutPiece(p, destination);
            if (capturedPiece != null)
            {
                Captured.Add(capturedPiece);
            }

            //Special Move: Castling - King Side
            if (p is King && destination.Column == origin.Column + 2)
            {
                Position towerOrigin = new Position(origin.Row, origin.Column + 3);
                Position towerDestination = new Position(origin.Row, origin.Column + 1);
                Piece tower = Board.RemovePiece(towerOrigin);
                tower.IncreaseNumberOfMoves();
                Board.PutPiece(tower, towerDestination);
            }

            //Special Move: Castling - Queen Side
            if (p is King && destination.Column == origin.Column - 2)
            {
                Position towerOrigin = new Position(origin.Row, origin.Column - 4);
                Position towerDestination = new Position(origin.Row, origin.Column - 1);
                Piece tower = Board.RemovePiece(towerOrigin);
                tower.IncreaseNumberOfMoves();
                Board.PutPiece(tower, towerDestination);
            }

            return capturedPiece;
        }

        private void UndoMove(Position origin, Position destination, Piece capturedPiece)
        {
            Piece p = Board.RemovePiece(destination);
            p.DecreaseNumberOfMoves();
            if(capturedPiece != null)
            {
                Board.PutPiece(capturedPiece, destination);
                Captured.Remove(capturedPiece);
            }
            Board.PutPiece(p, origin);

            //Special Move: Castling - King Side
            if (p is King && destination.Column == origin.Column + 2)
            {
                Position towerOrigin = new Position(origin.Row, origin.Column + 3);
                Position towerDestination = new Position(origin.Row, origin.Column + 1);
                Piece tower = Board.RemovePiece(towerOrigin);
                tower.DecreaseNumberOfMoves();
                Board.PutPiece(tower, towerDestination);
            }

            //Special Move: Castling - Queen Side
            if (p is King && destination.Column == origin.Column - 2)
            {
                Position towerOrigin = new Position(origin.Row, origin.Column - 4);
                Position towerDestination = new Position(origin.Row, origin.Column - 1);
                Piece tower = Board.RemovePiece(towerOrigin);
                tower.DecreaseNumberOfMoves();
                Board.PutPiece(tower, towerDestination);
            }
        }

        public void ValidateOriginPosition(Position pos)
        {
            if (Board.GetPiece(pos) == null)
            {
                throw new BoardException("There's no piece in the chosen origin position.");
            }
            
            if (ActualPlayer != Board.GetPiece(pos).Color)
            {
                throw new BoardException("The chosen piece isn't yours.");
            }

            if (!Board.GetPiece(pos).HasPossibleMoves())
            {
                throw new BoardException("The chosen piece is blocked.");
            }
        }

        public void ValidateDestinationPosition(Position origin, Position destination)
        {
            if (!Board.GetPiece(origin).CanMoveTo(destination))
            {
                throw new BoardException("Invalid destination position.");
            }
        }

        public HashSet<Piece> CapturedPieces(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece p in Captured)
            {
                if (p.Color == color)
                {
                    aux.Add(p);
                }
            }

            return aux;
        }

        public HashSet<Piece> PiecesOnBoard(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece p in Pieces)
            {
                if (p.Color == color)
                {
                    aux.Add(p);
                }
            }

            aux.ExceptWith(CapturedPieces(color));
            return aux;
        }

        private Color GetEnemyColor(Color color)
        {
            return color == Color.White ? Color.Black : Color.White;
        }

        public void PutNewPiece(char column, int row, Piece piece)
        {
            Board.PutPiece(piece, new ChessPosition(column, row).ToPosition());
            Pieces.Add(piece);
        }

        private void PutPieces()
        {
            PutNewPiece('a', 1, new Tower(Board, Color.White));
            PutNewPiece('b', 1, new Horse(Board, Color.White));
            PutNewPiece('c', 1, new Bishop(Board, Color.White));
            PutNewPiece('d', 1, new Queen(Board, Color.White));
            PutNewPiece('e', 1, new King(Board, Color.White, this));
            PutNewPiece('f', 1, new Bishop(Board, Color.White));
            PutNewPiece('g', 1, new Horse(Board, Color.White));
            PutNewPiece('h', 1, new Tower(Board, Color.White));
            PutNewPiece('a', 2, new Pawn(Board, Color.White));
            PutNewPiece('b', 2, new Pawn(Board, Color.White));
            PutNewPiece('c', 2, new Pawn(Board, Color.White));
            PutNewPiece('d', 2, new Pawn(Board, Color.White));
            PutNewPiece('e', 2, new Pawn(Board, Color.White));
            PutNewPiece('f', 2, new Pawn(Board, Color.White));
            PutNewPiece('g', 2, new Pawn(Board, Color.White));
            PutNewPiece('h', 2, new Pawn(Board, Color.White));

            PutNewPiece('a', 8, new Tower(Board, Color.Black));
            PutNewPiece('b', 8, new Horse(Board, Color.Black));
            PutNewPiece('c', 8, new Bishop(Board, Color.Black));
            PutNewPiece('d', 8, new Queen(Board, Color.Black));
            PutNewPiece('e', 8, new King(Board, Color.Black, this));
            PutNewPiece('f', 8, new Bishop(Board, Color.Black));
            PutNewPiece('g', 8, new Horse(Board, Color.Black));
            PutNewPiece('h', 8, new Tower(Board, Color.Black));
            PutNewPiece('a', 7, new Pawn(Board, Color.Black));
            PutNewPiece('b', 7, new Pawn(Board, Color.Black));
            PutNewPiece('c', 7, new Pawn(Board, Color.Black));
            PutNewPiece('d', 7, new Pawn(Board, Color.Black));
            PutNewPiece('e', 7, new Pawn(Board, Color.Black));
            PutNewPiece('f', 7, new Pawn(Board, Color.Black));
            PutNewPiece('g', 7, new Pawn(Board, Color.Black));
            PutNewPiece('h', 7, new Pawn(Board, Color.Black));
        }

        private Piece GetKing(Color color)
        {
            foreach (Piece p in PiecesOnBoard(color))
            {
                if (p is King)
                {
                    return p;
                }
            }
            return null;
        }

        public bool IsInCheck(Color color)
        {
            Piece king = GetKing(color);
            if (king == null)
            {
                throw new BoardException("There's no " + color + " King on the board.");
            }

            foreach (Piece p in PiecesOnBoard(GetEnemyColor(color)))
            {
                bool[,] mat = p.PossibleMoves();
                if(mat[king.Position.Row, king.Position.Column])
                {
                    return true;
                }
            }

            return false;
        }

        public bool TestCheckMate(Color color)
        {
            if (!IsInCheck(color))
            {
                return false;
            }

            foreach (Piece p in PiecesOnBoard(color))
            {
                bool[,] mat = p.PossibleMoves();
                for (int i = 0; i < p.Board.Rows; i++)
                {
                    for (int j = 0; j < p.Board.Columns; j++)
                    {
                        if(mat[i, j])
                        {
                            Position origin = p.Position;
                            Position destination = new Position(i, j);
                            Piece capturedPiece = MovePiece(origin, destination);
                            bool testCheck = IsInCheck(color);
                            UndoMove(origin, destination, capturedPiece);
                            if (!testCheck)
                            {
                                return false;
                            }
                        }
                    }
                }
            }

            return true;
        }
    }
}
