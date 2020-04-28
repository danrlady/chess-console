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
                ChessMatch match = new ChessMatch();

                while (!match.Finished)
                {
                    try
                    {
                        Console.Clear();
                        Screen.PrintMatch(match);

                        Console.WriteLine();
                        Console.Write("Origin: ");
                        Position origin = Screen.ReadChessPosition().ToPosition();
                        match.ValidateOriginPosition(origin);

                        bool[,] possibleMoves = match.Board.GetPiece(origin).PossibleMoves();

                        Console.Clear();
                        Screen.PrintBoard(match.Board, possibleMoves);

                        Console.WriteLine();
                        Console.Write("Destination: ");
                        Position destination = Screen.ReadChessPosition().ToPosition();
                        match.ValidateDestinationPosition(origin, destination);

                        match.MakeAMove(origin, destination);
                    }
                    catch (BoardException e)
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
                    }
                }
            }
            catch (BoardException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
