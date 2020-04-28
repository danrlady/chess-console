using System;
using System.Collections.Generic;
using Chess;
using GameBoard;

namespace Chess_Console
{
    class Screen
    {

        public static void PrintMatch(ChessMatch match)
        {
            PrintBoard(match.Board);
            Console.WriteLine();
            PrintCapturedPieces(match);
            Console.WriteLine();
            Console.WriteLine("Turn: " + match.Turn);
            Console.WriteLine(match.ActualPlayer + " player, please make a move");
        }

        public static void PrintCapturedPieces(ChessMatch match)
        {
            Console.WriteLine("Captured Pieces");
            Console.Write("White: ");
            PrintSet(match.CapturedPieces(Color.White));
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write("Black: ");
            PrintSet(match.CapturedPieces(Color.Black));
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
        }

        public static void PrintSet(HashSet<Piece> set)
        {
            Console.Write("[ ");
            foreach (Piece p in set)
            {
                Console.Write(p + " ");
            }
            Console.Write("]");
        }

        public static void PrintBoard(Board board)
        {
            for (int i = 0; i < board.Rows; i++)
            {
                Console.Write(8 - i + "  ");
                for (int j = 0; j < board.Columns; j++)
                {                    
                    PrintPiece(board.GetPiece(i, j));                
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine("   A B C D E F G H");
        }

        public static void PrintBoard(Board board, bool[,] possibleMoves)
        {
            ConsoleColor originalColor = Console.BackgroundColor;
            ConsoleColor alteredColor = ConsoleColor.DarkGray;

            for (int i = 0; i < board.Rows; i++)
            {
                Console.Write(8 - i + "  ");
                for (int j = 0; j < board.Columns; j++)
                {
                    Console.BackgroundColor = possibleMoves[i, j] ? alteredColor : originalColor;
                    PrintPiece(board.GetPiece(i, j));
                }
                Console.WriteLine();
                Console.BackgroundColor = originalColor;
            }
            Console.WriteLine();
            Console.WriteLine("   A B C D E F G H");
            Console.BackgroundColor = originalColor;
        }

        public static void PrintPiece(Piece p)
        {
            if (p == null)
            {
                Console.Write("- ");
            }
            else
            {
                if (p.Color == Color.White)
                {
                    Console.Write(p);
                }
                else
                {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.Write(p);
                    Console.ForegroundColor = aux;
                }
                Console.Write(" ");
            }

        }

        public static ChessPosition ReadChessPosition()
        {
            string s = Console.ReadLine();
            char column = s[0];
            int row = int.Parse(s[1] + "");
            return new ChessPosition(column, row);
        }
    }
}
