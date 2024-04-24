
using Connect4Games;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Numerics;
using System.Transactions;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Connect4Games
{
    // Game Class
    public class Connect4Game
    {
        private Connect4GameBoard Connect4GameBoard { get; set; }
        private Player player1; // player 1 which will be X
        private Player player2; // player which will be O
        private Player currentPlayer; // Current player
        public Connect4Game()
        {
            Console.WriteLine();
            Console.WriteLine("Alright... \nWelcome to Your Connect 4 Game");
            Console.WriteLine("Please Select a mode from below:\n(Input 1 or 2)");
            Console.WriteLine("1. Play against computer");
            Console.WriteLine("2. Play against another player");
            Console.WriteLine("Enter your mode number(1 or 2): ");
            bool validInput = false;
            int number = 0;
            // code to ensure enters lid mode as an input
            while (!validInput)
            {
                string mode = (Console.ReadLine());
                if (!int.TryParse(mode, out number))
                {
                    Console.WriteLine("Invalid Input. Please enter a valid number(1 or 2):");
                }
                else if (mode != "1" && mode != "2")
                {
                    Console.WriteLine("Invalid input. Please enter either 1 or 2.");
                }
                else
                {
                    validInput = true;
                }
                // mode = 1 means player vs computer player
                if (mode == "1")
                {
                    Console.WriteLine("Player 1, Please Enter Your Desired Name: ");
                    string name1 = Console.ReadLine();
                    player1 = new Human(name1, 'X');
                    player2 = new ComputerPlayer("Computer Player", 'O');
                }
                // mode = 1 means player vs another player
                else if (mode == "2")
                {
                    Console.WriteLine("Player 1, Please Enter Your Desired Name: ");
                    string name1 = Console.ReadLine();
                    Console.WriteLine("Player 2, Please Enter Your Desired  Name: ");
                    string name2 = Console.ReadLine();
                    player1 = new Human(name1, 'X');
                    player2 = new Human(name2, '0');

                }

            }
            Connect4GameBoard = new Connect4GameBoard();
            Console.WriteLine("\nThank You!, Now below is what your board's layout looks like. \nEnjoy your game.");

            currentPlayer = player1; // current player will always be player 1 which is player x

        }
        // method to start game
        public void Start()
        {
            Console.WriteLine();
            Connect4GameBoard.InitializeBoard(); // initialize board before displaying
            // Main game loop
            while (!Connect4GameBoard.IsGameOver())
            {
                Connect4GameBoard.DisplayBoard(); // Print the current state of the board
                Console.WriteLine($"Player {currentPlayer.Name} (Player {currentPlayer.Char}'s) turn:"); // Display whose turn it is
                currentPlayer.Play(Connect4GameBoard); // Let the current player play
                currentPlayer = (currentPlayer == player1) ? player2 : player1; // Switches player's turn
            }

            // The game over control
            if (Connect4GameBoard.CheckWin())

            {
                Connect4GameBoard.DisplayBoard(); // display board to see who won
                if (currentPlayer == player1)
                {
                    currentPlayer.Char = '0';
                    currentPlayer.Name = player2.Name;
                }
                else
                {
                    currentPlayer.Char = 'X';
                    currentPlayer.Name = player1.Name;
                }

                Console.WriteLine($"Player {currentPlayer.Name} (Player {currentPlayer.Char}) wins!"); // Display the winner
                Console.ReadKey();
                Console.WriteLine("\nThanks For Playing Our Game");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("It's a draw!"); // Display if it is a draw
                Console.ReadKey();
                Console.WriteLine("\nThanks For Playing Our Game");
                Console.ReadKey();
            }
        }
    }

}
// board Class
public class Connect4GameBoard
{
    private static int Rows = 6;
    private static int Columns = 7;
    public char[,] board = new char[Rows, Columns]; // an array of char with size of Rows * Columns
    public void InitializeBoard() // method that initializes my board with #
    {
        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Columns; j++)
            {
                board[i, j] = '#';
            }
        }
    }
    public void DisplayBoard() // method that displays my board
    {
        for (int i = 0; i < Rows; i++)
        {
            Console.Write("| ");
            for (int j = 0; j < Columns; j++)
            {

                Console.Write(board[i, j] + "  ");
            }
            Console.Write("\b |");
            Console.WriteLine();
        }
        Console.WriteLine("  1  2  3  4  5  6  7");
        Console.WriteLine();
    }

    public bool DropPiece(int col, Player playerTurn) // method to drop a char
    {
        for (int row = Rows - 1; row >= 0; row--) // starts from bottom row
        {
            if (board[row, col] == '#')
            {
                board[row, col] = playerTurn.Char; // replaces exact spot in board with char if it has #
                return true;
            }
        }
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
                if (board[row, col] != '#' &&
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
                if (board[row, col] != '#' &&
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
                if (board[row, col] != '#' &&
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
                if (board[row, col] != '#' &&
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
                if (board[row, col] == '#')
                {
                    return false; // Return false if there is still space
                }
            }
        }
        return true; // Return true if the board is full
    }
    // Check for win or draw
    public bool IsGameOver() // checks if game is over either by checking if there is a winner or if board is full
    {
        return CheckWin() || IsBoardFull();
    }

}

// my player class
public abstract class Player
{
    public string Name { get; set; }
    public char Char { get; set; }
    public Player(string name, char sign)
    {
        Name = name;
        Char = sign;
    }
    public abstract void Play(Connect4GameBoard board);
}

// my human player class derived from player class
public class Human : Player
{
    public Human(string name, char sign) : base(name, sign)
    {
    }
    public override void Play(Connect4GameBoard board) // overriding play method from base class
    {
        bool madeAMove = false;
        while (!madeAMove)
        {
            Console.WriteLine("Enter a column number from 1 to 7: ");
            int colNo;
            if (int.TryParse(Console.ReadLine(), out colNo))
            {
                if (colNo >= 1 && colNo <= 7)
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
                    Console.WriteLine("Please enter a column number from the range 1 - 7.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
            }
        }
    }
}

// my computer player class
public class ComputerPlayer : Player
{
    public ComputerPlayer(string name, char sign) : base(name, sign)
    {
    }
    // Method to make a move on the board
    public override void Play(Connect4GameBoard board)  // overriding play method from base class
    {
        Random randomNo = new Random();
        Console.WriteLine("Enter a column number from 1 to 7: ");
        int column;
        do
        {
            column = randomNo.Next(1, 7); // random number from 0 to 7
            Console.WriteLine(column);
        } while (!board.DropPiece(column - 1, this)); // Keep generating until a valid move is made
    }
}
class Program
{
    static void Main(string[] args)
    {
        Connect4Game game = new Connect4Game();
        game.Start(); // start my game.
    }
}




