// This is my project folder.
//Connect Four Game by Chidiebere Nwigwe and Anthony Odinukwe.

using System.Data.Common;
using System.Numerics;

namespace Connect4Game
{
    public class GameBoard
    {
        public static int Rows = 6;
        public static int Columns = 7;
        public char[,] board = new char[Rows, Columns];

        public void InitializeBoard()
        {
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    board[i, j] = '#';
                }
            }
        }
        public void DisplayBoard()
        {
            // InitializeBoard();
            for (int i = 0; i < Rows; i++)
            {
                Console.Write("| ");
                for (int j = 0; j < Columns; j++)
                {

                    Console.Write(board[i, j] + "  ");
                    // Console.Write("#  ");
                }
                Console.Write("\b |");
                Console.WriteLine();
            }
            Console.WriteLine("  1  2  3  4  5  6  7");
            Console.WriteLine();
        }

        public void DropPiece(int col)
        {
            int colu = col - 1;

            //board.DisplayBoard();
            //InitializeBoard();

            for (int row = Rows - 1; row > 0; row--)
            {
                //  Console.Write("| ");
                //   for (int j = 0; j < Columns; j++)
                {
                    if (board[row, colu] == '#')
                    {


                        board[row, colu] = 'X';
                        break;
                    }
                    //      Console.Write("\b |");
                    //      Console.WriteLine();
                }
                //   Console.WriteLine("  1  2  3  4  5  6  7");

            }
            DisplayBoard();
        }


    }



    class ControllerClass
    {
        //public strin
        public string Name { get; set; }
        public string currentPlayer { get; set; }
        private const int Rows = 6;
        private const int Columns = 7;
        public GameBoard board { get; set; }
        public ControllerClass(string name)
        {
            Name = name;
            currentPlayer = "X";
            board = new GameBoard();
        }
        public string GetThePlayer()
        {
            return Name;
        }
        public void DropPiece(int col)
        {
            int colu = col;

            //board.DisplayBoard();
            board.InitializeBoard();


        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            GameBoard board = new GameBoard();
            board.InitializeBoard();
            board.DisplayBoard();
            Console.WriteLine();

            //for(int i = 0)
            board.DropPiece(1);
            board.DropPiece(2);
            board.DropPiece(7);
            board.DropPiece(1);

            Console.WriteLine();
            Console.WriteLine("Alright... \nWelcome to Your Connect 4 Game");
            Console.WriteLine("Player 1, Please Enter Your  Name: ");
            string player1 = Console.ReadLine();
            Console.WriteLine(player1);
            Console.WriteLine("Player 2, Please Enter Your  Name: ");
            string player2 = Console.ReadLine();
            Console.WriteLine(player2);


            Console.ReadKey();
        }
    }
}