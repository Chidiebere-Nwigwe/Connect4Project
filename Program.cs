using System;
using System.ComponentModel.DataAnnotations.Schema;

public enum Player
{
    None,
    Player1,
    Player2
}

// WILL define my board classs later
class Board
{

}
class Gamer
{
    public char Symbol { get; protected set; } // Player's symbol (X or O)

    public Gamer(char symbol)
    {
        Symbol = symbol;
    }

    // Method to make a move on the board 
    public virtual void MakeMove(Board board)
    {
        throw new NotImplementedException(); // Throw an exception if not implemented
    }
}

class HumanPlayer : Gamer
{
    // Constructor to set the player's symbol
    public HumanPlayer(char symbol) : base(symbol)
    {
    }

    // Method to make a move on the board
    public override void MakeMove(Board board)
    {
        bool validMove = false;
        while (!validMove)
        {
            Console.Write("Enter column number (1-7): ");
            int column;
            // Validate user input for column number
            if (int.TryParse(Console.ReadLine(), out column) && column >= 1 && column <= 7)
            {
                column--;

                // Attempt to drop a piece in the specified column
                if (board.DropPiece(column, Symbol))
                {
                    validMove = true;
                }
                else
                {
                    Console.WriteLine("Column is full. Please choose another column.");
                }
            }
            else
            {
                Console.WriteLine("Please enter a number between 1 and 7.");
            }
        }
    }
}

class ComputerPlayer : Gamer
{
    // Constructor to set the player's symbol
    public ComputerPlayer(char symbol) : base(symbol)
    {
    }

    // Method to make a move on the board
    public override void MakeMove(Board board)
    {
        Random random = new Random();
        int column;
        do
        {
            column = random.Next(0, 7); // Generate a random column index
        } while (!board.DropPiece(column, Symbol)); // Keep generating until a valid move is made
    }
}

public class Connect4Game
{
    private const int Rows = 6;
    private const int Columns = 7;
    private Player[,] board = new Player[Rows, Columns];
    private Player currentPlayer = Player.Player1;

    public Connect4Game()
    {
        InitializeBoard();
    }

    public void InitializeBoard()
    {
        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Columns; j++)
            {
                board[i, j] = Player.None;
            }
        }
    }

    public void DisplayBoard()
    {
        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Columns; j++)
            {
                char symbol = board[i, j] switch
                {
                    Player.None => '#',
                    Player.Player1 => 'X',
                    Player.Player2 => 'O',
                    _ => throw new NotImplementedException()
                };
                Console.Write(symbol + " ");
            }
            Console.WriteLine();
        }
    }

    public bool DropPiece(int column)
    {
        if (column < 0 || column >= Columns || board[0, column] != Player.None)
        {
            return false; // Invalid move
        }

        for (int row = Rows - 1; row >= 0; row--)
        {
            if (board[row, column] == Player.None)
            {
                board[row, column] = currentPlayer;
                return true;
            }
        }

        return false; // Column is full
    }

    public bool CheckWin(Player player)
    {
        // Implement win condition checking logic
        return false;
    }

    public Player CurrentPlayer => currentPlayer;

    public void SwitchPlayer()
    {
        currentPlayer = currentPlayer == Player.Player1 ? Player.Player2 : Player.Player1;
    }
}

class Program
{
    static void Main(string[] args)
    {
        Connect4Game game = new Connect4Game();
        game.DisplayBoard();

        while (true)
        {
            Console.WriteLine($"Player {game.CurrentPlayer}, choose a column (0-6): ");
            int column;
            if (!int.TryParse(Console.ReadLine(), out column) || column < 0 || column >= 7)
            {
                Console.WriteLine("Invalid input. Please enter a number between 0 and 6.");
                continue;
            }
            if (game.DropPiece(column))
            {
                game.DisplayBoard();
                if (game.CheckWin(game.CurrentPlayer))
                {
                    Console.WriteLine($"Player {game.CurrentPlayer} wins!");
                    break;
                }
                game.SwitchPlayer();
            }
            else
            {
                Console.WriteLine("Invalid move. Try again.");
            }
        }
    }
}

/*
namespace Connect4Game
{
    // Main program class
    class Program
    {
        static void Main(string[] args)
        {
            Connect4Game game = new Connect4Game();
            game.Start();
        }
    }
    // Class representing the Connect 4 game
    class Connect4Game
    {
        private Board board;
        private Player player1; // Player 1
        private Player player2; // Player 2
        private Player currentPlayer; // Current player

        public Connect4Game()
        {
            Console.WriteLine("Welcome to Connect 4!");
            Console.WriteLine("Select game mode:");
            Console.WriteLine("1. Play against computer");
            Console.WriteLine("2. Play against another player");
            Console.Write("Enter your choice: ");
            int choice = int.Parse(Console.ReadLine());

            // Initialize players based on the users choice
            if (choice == 1)
            {
                player1 = new HumanPlayer('X');
                player2 = new ComputerPlayer('O');
            }
            else if (choice == 2)
            {
                player1 = new HumanPlayer('X');
                player2 = new HumanPlayer('O');
            }
            else
            {
                Console.WriteLine("Invalid choice. Exiting...");
                Environment.Exit(0);
            }

            board = new Board();
            currentPlayer = player1; // Player 'X' always starts the game
        }
        public void Start()
        {
            // Main game loop
            while (!board.IsGameOver())
            {
                Console.Clear(); // Clears the console
                board.PrintBoard(); // Print the current state of the board
                Console.WriteLine($"Player {currentPlayer.Symbol}'s turn:"); // Display whose turn it is
                currentPlayer.MakeMove(board); // Let the current player make a move
                currentPlayer = (currentPlayer == player1) ? player2 : player1; // Switches players
            }

            // The game over control
            Console.Clear(); // Clears the console
            board.PrintBoard(); // Prints the final state of the board
            if (board.CheckWin())
            {
                Console.WriteLine($"Player {currentPlayer.Symbol} wins!"); // Display the winner
            }
            else
            {
                Console.WriteLine("It's a draw!"); // Display if it is a draw
            }
        }
    }

    // Class representing the  board
    class Board
    {
        private char[,] board = new char[6, 7]; // Sets the dimensions of the board (2D array)

        public Board()
        {
            InitializeBoard();
        }
        private void InitializeBoard()
        {
            for (int row = 0; row < 6; row++)
            {
                for (int col = 0; col < 7; col++)
                {
                    board[row, col] = ' ';
                }
            }
        }

        // Prints the current state of the board
        public void PrintBoard()
        {
            Console.WriteLine("  1 2 3 4 5 6 7");
            for (int row = 0; row < 6; row++)
            {
                Console.Write("| ");
                for (int col = 0; col < 7; col++)
                {
                    Console.Write(board[row, col] + " ");
                }
                Console.WriteLine("|");
            }
            Console.WriteLine("---------------");
        }

        // Drops the symbol into the specified column
        public bool DropPiece(int column, char symbol)
        {
            for (int row = 5; row >= 0; row--)
            {
                if (board[row, column] == ' ')
                {
                    board[row, column] = symbol;
                    return true;
                }
            }
            return false;
        }

        // Check if there's a win  on the board
        public bool CheckWin()
        {
            // Checks rows
            for (int row = 0; row < 6; row++)
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

            for (int col = 0; col < 7; col++)
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

            // Checks the diagonals
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
                for (int col = 3; col < 7; col++)
                {
                    if (board[row, col] != ' ' &&
                        board[row, col] == board[row + 1, col - 1] &&
                        board[row, col] == board[row + 2, col - 2] &&
                        board[row, col] == board[row + 3, col - 3])
                    {
                        return true; // Return true if there's a win
                    }
                }
            }

            return false;
        }

        // Check if the game is over (win or draw)
        public bool IsGameOver()
        {
            return CheckWin() || IsBoardFull();
        }

        // Check if the board is full
        private bool IsBoardFull()
        {
            for (int row = 0; row < 6; row++)
            {
                for (int col = 0; col < 7; col++)
                {
                    if (board[row, col] == ' ')
                    {
                        return false; // Return false if there's an empty space
                    }
                }
            }
            return true; // Return true if the board is full, indicating a draw
        }
    }

    class Player
    {
        public char Symbol { get; protected set; } // Player's symbol (X or O)

        public Player(char symbol)
        {
            Symbol = symbol;
        }

        // Method to make a move on the board 
        public virtual void MakeMove(Board board)
        {
            throw new NotImplementedException(); // Throw an exception if not implemented
        }
    }

    class HumanPlayer : Player
    {
        // Constructor to set the player's symbol
        public HumanPlayer(char symbol) : base(symbol)
        {
        }

        // Method to make a move on the board
        public override void MakeMove(Board board)
        {
            bool validMove = false;
            while (!validMove)
            {
                Console.Write("Enter column number (1-7): ");
                int column;
                // Validate user input for column number
                if (int.TryParse(Console.ReadLine(), out column) && column >= 1 && column <= 7)
                {
                    column--;

                    // Attempt to drop a piece in the specified column
                    if (board.DropPiece(column, Symbol))
                    {
                        validMove = true;
                    }
                    else
                    {
                        Console.WriteLine("Column is full. Please choose another column.");
                    }
                }
                else
                {
                    Console.WriteLine("Please enter a number between 1 and 7.");
                }
            }
        }
    }

    class ComputerPlayer : Player
    {
        // Constructor to set the player's symbol
        public ComputerPlayer(char symbol) : base(symbol)
        {
        }

        // Method to make a move on the board
        public override void MakeMove(Board board)
        {
            Random random = new Random();
            int column;
            do
            {
                column = random.Next(0, 7); // Generate a random column index
            } while (!board.DropPiece(column, Symbol)); // Keep generating until a valid move is made
        }
    }
}*/