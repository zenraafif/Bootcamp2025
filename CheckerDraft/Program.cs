using System;
using System.Collections.Generic;
using CheckerDraft;

class Program
{
    static void Main(string[] args)
    {
        // Initialize board
        IBoard board = new Board();
        // board.PrintBoard();

        // Create players
        var players = new List<IPlayer>
        {
            new Player("Player1", PieceColor.Black),
            new Player("Player2", PieceColor.White)
        };

        // Initialize game controller
        var game = new GameController(board, players);
        game.StartGame();

        while (true)
        {
            Console.WriteLine();
            Console.WriteLine($"Current Player: {game.CurrentPlayer.Name}");

            // Ask for move
            Console.Write("Enter Move (fromX fromY toX toY) or type 'exit': ");
            string input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("No input detected. Please try again.");
                continue;
            }

            if (input.Trim().Equals("exit", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Exiting game...");
                break;
            }

            // Split input safely (ignoring extra spaces)
            var parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length != 4 ||
                !int.TryParse(parts[0], out int fromX) ||
                !int.TryParse(parts[1], out int fromY) ||
                !int.TryParse(parts[2], out int toX) ||
                !int.TryParse(parts[3], out int toY))
            {
                Console.WriteLine("Please enter 4 numbers separated by spaces.");
                continue;
            }

            // Create move
            var move = new Move(
                new Position(fromX, fromY),
                new Position(toX, toY)
            );

            // Try applying move
            try
            {
                game.ApplyMove(move);
                board.PrintBoard();
                game.SwitchTurn();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid move: " + ex.Message);
                Console.ReadKey();
            }
        }
    }
}
