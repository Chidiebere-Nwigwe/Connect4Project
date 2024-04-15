using System;

public enum Player
{
    None,
    Player1,
    Player2
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