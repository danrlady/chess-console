using System;
using GameBoard;

namespace Chess
{
    class ChessMatch
    {
        public Board Board { get; private set; }
        public int Turn { get; private set; }
        public Color ActualPlayer { get; private set; }
        public bool Finished { get; private set; }

        public ChessMatch()
        {
            Board = new Board(8, 8);
            Turn = 1;
            ActualPlayer = Color.White;
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

        private void PutPieces()
        {
            Board.PutPiece(new Tower(Board, Color.White), new ChessPosition('c', 1).ToPosition());
            Board.PutPiece(new Bishop(Board, Color.White), new ChessPosition('c', 2).ToPosition());
            Board.PutPiece(new Horse(Board, Color.White), new ChessPosition('d', 2).ToPosition());
            Board.PutPiece(new Pawn(Board, Color.White), new ChessPosition('e', 2).ToPosition());
            Board.PutPiece(new Tower(Board, Color.White), new ChessPosition('e', 1).ToPosition());
            Board.PutPiece(new King(Board, Color.White), new ChessPosition('d', 1).ToPosition());

            Board.PutPiece(new Tower(Board, Color.Black), new ChessPosition('c', 7).ToPosition());
            Board.PutPiece(new Tower(Board, Color.Black), new ChessPosition('c', 8).ToPosition());
            Board.PutPiece(new Pawn(Board, Color.Black), new ChessPosition('d', 7).ToPosition());
            Board.PutPiece(new Tower(Board, Color.Black), new ChessPosition('e', 7).ToPosition());
            Board.PutPiece(new Tower(Board, Color.Black), new ChessPosition('e', 8).ToPosition());
            Board.PutPiece(new King(Board, Color.Black), new ChessPosition('d', 8).ToPosition());
        }
    }
}
