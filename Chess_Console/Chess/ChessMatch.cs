using GameBoard;

namespace Chess
{
    class ChessMatch
    {
        public Board Board { get; private set; }
        private int _turn;
        private Color _actualPlayer;
        public bool Finished { get; private set; }

        public ChessMatch()
        {
            Board = new Board(8, 8);
            _turn = 1;
            _actualPlayer = Color.White;
            PutPieces();
        }

        public void MovePiece(Position origin, Position destination)
        {
            Piece p = Board.RemovePiece(origin);
            p.IncrementNumberOfMoves();
            Piece capturedPiece = Board.RemovePiece(destination);
            Board.PutPiece(p, destination);
        }

        private void PutPieces()
        {
            Board.PutPiece(new Tower(Board, Color.White), new ChessPosition('c', 1).ToPosition());
            Board.PutPiece(new Tower(Board, Color.White), new ChessPosition('c', 2).ToPosition());
            Board.PutPiece(new Tower(Board, Color.White), new ChessPosition('d', 2).ToPosition());
            Board.PutPiece(new Tower(Board, Color.White), new ChessPosition('e', 2).ToPosition());
            Board.PutPiece(new Tower(Board, Color.White), new ChessPosition('e', 1).ToPosition());
            Board.PutPiece(new King(Board, Color.White), new ChessPosition('d', 1).ToPosition());

            Board.PutPiece(new Tower(Board, Color.Black), new ChessPosition('c', 7).ToPosition());
            Board.PutPiece(new Tower(Board, Color.Black), new ChessPosition('c', 8).ToPosition());
            Board.PutPiece(new Tower(Board, Color.Black), new ChessPosition('d', 7).ToPosition());
            Board.PutPiece(new Tower(Board, Color.Black), new ChessPosition('e', 7).ToPosition());
            Board.PutPiece(new Tower(Board, Color.Black), new ChessPosition('e', 8).ToPosition());
            Board.PutPiece(new King(Board, Color.Black), new ChessPosition('d', 8).ToPosition());
        }
    }
}
