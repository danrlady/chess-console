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

        public ChessMatch()
        {
            Board = new Board(8, 8);
            Turn = 1;
            ActualPlayer = Color.White;
            Pieces = new HashSet<Piece>();
            Captured = new HashSet<Piece>();
            PutPieces();
        }

        public void MakeAMove(Position origin, Position destination)
        {
            MovePiece(origin, destination);
            Turn++;
            ChangePlayer();
        }

        private void ChangePlayer()
        {
            ActualPlayer = ActualPlayer == Color.White ? Color.Black : Color.White;
        }

        public void MovePiece(Position origin, Position destination)
        {
            Piece p = Board.RemovePiece(origin);
            p.IncrementNumberOfMoves();
            Piece capturedPiece = Board.RemovePiece(destination);
            Board.PutPiece(p, destination);
            if (capturedPiece != null)
            {
                Captured.Add(capturedPiece);
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
            foreach (Piece p in Captured)
            {
                if (p.Color == color)
                {
                    aux.Add(p);
                }
            }

            aux.ExceptWith(CapturedPieces(color));
            return aux;
        }

        public void PutNewPiece(char column, int row, Piece piece)
        {
            Board.PutPiece(piece, new ChessPosition(column, row).ToPosition());
            Pieces.Add(piece);
        }

        private void PutPieces()
        {
            PutNewPiece('c', 1, new Tower(Board, Color.White));
            PutNewPiece('d', 1, new King(Board, Color.White));
            PutNewPiece('e', 1, new Tower(Board, Color.White));
            PutNewPiece('c', 2, new Pawn(Board, Color.White));
            PutNewPiece('d', 2, new Pawn(Board, Color.White));
            PutNewPiece('e', 2, new Pawn(Board, Color.White));

            PutNewPiece('c', 8, new Tower(Board, Color.Black));
            PutNewPiece('d', 8, new King(Board, Color.Black));
            PutNewPiece('e', 8, new Tower(Board, Color.Black));
            PutNewPiece('c', 7, new Pawn(Board, Color.Black));
            PutNewPiece('d', 7, new Pawn(Board, Color.Black));
            PutNewPiece('e', 7, new Pawn(Board, Color.Black));
        }
    }
}
