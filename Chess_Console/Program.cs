using Chess;
using GameBoard;
using System;

namespace Chess_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Board board = new Board(8, 8);
                board.PutPiece(new King(board, Color.Black), new Position(1, 1));
                board.PutPiece(new Queen(board, Color.Black), new Position(2, 1));
                board.PutPiece(new Tower(board, Color.Black), new Position(3, 1));
                Screen.PrintBoard(board);
            }
            catch(BoardException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
