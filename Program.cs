
using Connect4Games;
using System.Data.Common;
using System.Numerics;
using System.Transactions;

namespace Connect4Games
{
    // Game Class
    public class Connect4Game
    {
        public Connect4GameBoard Connect4GameBoard { get; set; }
        private Player player1; // player 1 which will be X
        private Player player2; // player which will be O
        private Player currentPlayer; // Current player
        public Connect4Game()
        {
            string name1 = "Chidi";
            string name2 = "Tony";
            Connect4GameBoard = new Connect4GameBoard();
            player1 = new Human(name1, 'X');
            player2 = new Human(name2, '0');
            currentPlayer = player1;
        }


        public void Start()
        {
            Console.WriteLine();
            Console.WriteLine("Alright... \nWelcome to Your Connect 4 Game");
            //  Console.WriteLine("Player 1, Please Enter Your  Name: ");
            ////  player1.Name = Console.ReadLine();
            // Console.WriteLine(player1);
            //  Console.WriteLine("Player 2, Please Enter Your  Name: ");
            //  player2.Name = Console.ReadLine();
            // Console.WriteLine(player2);
            // Main game loop
            Connect4GameBoard.InitializeBoard();

            while (Connect4GameBoard.IsGameOver() == true)
            {
                //Console.Clear(); // Clears the console
                Connect4GameBoard.DisplayBoard(); // Print the current state of the board
                Console.WriteLine($"Player {currentPlayer.Char}'s turn:"); // Display whose turn it is
                currentPlayer.Play(Connect4GameBoard); // Let the current player make a move
                currentPlayer = (currentPlayer == player1) ? player2 : player1; // Switches players
            }

            // The game over control
            //Console.Clear(); // Clears the console
            //Connect4GameBoard.DisplayBoard(); // Prints the final state of the board
            if (Connect4GameBoard.CheckWin())
            {
                Console.WriteLine($"Player {currentPlayer.Char} wins!"); // Display the winner
            }
            else
            {
                Console.WriteLine("It's a draw!"); // Display if it is a draw
            }
        }
    }

}



// board Class
public class Connect4GameBoard
{
    public static int Rows = 6;
    public static int Columns = 7;
    public char[,] board = new char[Rows, Columns];
    public int turn = 0;

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

    public bool DropPiece(int col, Human playerTurn)
    {
        int colu = col - 1;


        for (int row = Rows - 1; row >= 0; row--)
        {
            if (board[row, col] == '#')
            {


                //board[row, colu] = char.Parse(player[turn]);
                board[row, col] = playerTurn.Char;
                Console.WriteLine(playerTurn.Char);
                return true;
                break;
            }
        }

        //DisplayBoard();
        return false;

    }

    public bool CheckWin()
    {
        // check win in rows
        // check win in rows horizontally
        for (int row = 0; row < Rows; row++)
        {

            for (int col = 0; col < 4; col++)
            {
                if (board[row, col] != ' ' &&
                    board[row, col] == board[row, col + 1] &&
                    board[row, col] == board[row, col + 2] &&
                    board[row, col] == board[row, col + 3])
                {
                    return true;
                }
            }
        }

        // check win in rows vertically
        for (int col = 0; col < Columns; col++)
        {
            for (int row = 0; row < 3; row++)
            {
                if (board[row, col] != ' ' &&
                    board[row, col] == board[row + 1, col] &&
                    board[row, col] == board[row + 2, col] &&
                    board[row, col] == board[row + 3, col])
                {
                    return true;
                }
            }
        }

        // check win diagonally

        for (int row = 0; row < 3; row++)
        {
            for (int col = 0; col < 4; col++)
            {
                if (board[row, col] != ' ' &&
                    board[row, col] == board[row + 1, col + 1] &&
                    board[row, col] == board[row + 2, col + 2] &&
                    board[row, col] == board[row + 3, col + 3])
                {
                    return true;
                }
            }
        }

        for (int row = 0; row < 3; row++)
        {
            for (int col = 3; col < Columns; col++)
            {
                if (board[row, col] != ' ' &&
                    board[row, col] == board[row + 1, col - 1] &&
                    board[row, col] == board[row + 2, col - 2] &&
                    board[row, col] == board[row + 3, col - 3])
                {
                    return true;
                }
            }
        }

        return false;
    }
    // Check if the board is full
    private bool IsBoardFull()
    {
        for (int row = 0; row < Rows; row++)
        {
            for (int col = 0; col < Columns; col++)
            {
                if (board[row, col] == ' ')
                {
                    return false; // Return false if there is still space
                }
            }
        }
        return true; // Return true if the board is full
    }
    // Check for win or draw
    public bool IsGameOver()
    {
        return CheckWin() || IsBoardFull();
    }

}




public abstract class Player
{
    //public strin
    public string Name { get; set; }
    public char Char { get; set; }
    public Player(string name, char sign)
    {
        Name = name;
        Char = sign;
    }
    public abstract void Play(Connect4GameBoard board);
}

public class Human : Player
{
    public Human(string name, char sign) : base(name, sign)
    {

    }
    public override void Play(Connect4GameBoard board)
    {
        bool madeAMove = false;
        while (!madeAMove)
        {
            Console.WriteLine("Enter a column number from 1 to 7: ");
            int colNo = Int32.Parse(Console.ReadLine());
            if ((colNo >= 1) && (colNo <= 7))
            {
                colNo -= 1;
                if (board.DropPiece(colNo, this))
                {
                    madeAMove = true;
                }
                else
                {
                    Console.WriteLine("Column is full. Please enter another column number.");
                }
            }
            else
            {
                Console.WriteLine("Please Enter A column from Range of 1 - 7.");

            }


        }


    }


}

class Program
{
    static void Main(string[] args)
    {
        /* Connect4GameBoard board = new Connect4GameBoard();
         board.InitializeBoard();
         board.DisplayBoard();
         Console.WriteLine();

         //for(int i = 0; i < 42; i++){
           //  int j = 1
           //if( j == 7){  }
     //}
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


         Console.ReadKey();*/

        Connect4Game game = new Connect4Game();
        game.Start();
    }
}