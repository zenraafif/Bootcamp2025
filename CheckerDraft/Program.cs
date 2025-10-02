using System;
using System.Collections.Generic;
using System.Data;
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
        // subscribe to event
        void OnMoveAppliedHandler(ISquare from, ISquare to)
        {
            Console.WriteLine("Piece moved from " + from.Position + " to " + to.Position);
        }
        game.OnMoveApplied += OnMoveAppliedHandler;


        game.StartGame();

        while (true)
        {
            Console.WriteLine();
            Console.WriteLine($"Current Player: {game.CurrentPlayer.Name}");

                        // GET ALL VALID
            var movable = game.GetMovablePieces(game.CurrentPlayer.Color);

            foreach (var kvp in movable)
            {
                Console.WriteLine($"Piece at ({kvp.Key.Position.X},{kvp.Key.Position.Y}) can move to: " +
                    string.Join(", ", kvp.Value.Select(m => $"({m.x},{m.y})")));
            }

            // Movable list pieces
            int index = 1;
            Console.WriteLine("Select a piece you want to move :");

            // Put all movable pieces into a list so we can access by index
            var movableList = movable.ToList();

            // Show options
            foreach (var kvp in movableList)
            {
                Console.WriteLine($"{index}. {kvp.Key.Position.X},{kvp.Key.Position.Y}");
                index++;
            }


            // Read user input
            Console.Write("Enter number: ");
            string? input = Console.ReadLine();


            if (int.TryParse(input, out int choice) && choice > 0 && choice <= movableList.Count)
            {
                // Select piece
                var selectedPiece = movableList[choice - 1].Key;
                var validMoves = movableList[choice - 1].Value;

                Console.WriteLine($"You selected piece at {selectedPiece.Position.X},{selectedPiece.Position.Y}");
                Console.WriteLine("Now, select a position you want to move :");
                int indexPosition = 1;
                foreach (var kvp in movable)
                {
                    Console.WriteLine(
                        $"{indexPosition}. {string.Join(", ", kvp.Value.Select(m => $"({m.x},{m.y})"))}"
                    );
                    indexPosition++;
                }
                Console.WriteLine("Enter a number : ");
            
            }
            else
            {
                Console.WriteLine("Invalid selection.");
            }

            // Ask for move
            Console.Write("Enter Move (fromX fromY toX toY) or type 'exit': ");
            string inputt = Console.ReadLine();

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

            // if (parts.Length != 4 ||
            //     !int.TryParse(parts[0], out int fromX) ||
            //     !int.TryParse(parts[1], out int fromY) ||
            //     !int.TryParse(parts[2], out int toX) ||
            //     !int.TryParse(parts[3], out int toY))
            // {
            //     Console.WriteLine("Please enter 4 numbers separated by spaces.");
            //     continue;
            // }

            // Piece

            int fromX = 1;
            int fromY = 2;

            int toX = 2;
            int toY = 3;


            // Create move
            var move = new Move(
                new Position(fromX, fromY),
                new Position(toX, toY)
            );
            



            // Try applying move
            try
            {
                if (game.ValidateMove(move))
                {
                    game.ApplyMove(move);
                    // if (!game.IsGameOver(game.CurrentPlayer.Color))  
                    // {
                    board.PrintBoard();
                    // game.SwitchTurn();
                    // }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid move: " + ex.Message);
                Console.ReadKey();
            }
        }
    }
}