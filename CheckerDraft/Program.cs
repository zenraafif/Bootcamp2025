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

            // Show all pieces and their moves
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

//////            // Program.cs (or Project.cs) right after you compute movable pieces
            var captures = game.GetAllCaptures(game.CurrentPlayer.Color);
            if (captures.Any())
            {
                Console.WriteLine("DEBUG: Captures detected by logic:");
                foreach (var c in captures)
                {
                    var from = c.From;
                    var cap = c.Captured;
                    var to = c.To;
                    Console.WriteLine($"  From ({from.X},{from.Y}) captures ({cap.X},{cap.Y}) -> lands ({to.X},{to.Y})");
                    // Also print pieces/emptiness around these coordinates:
                    var fromSq = board.GetSquare(from.X, from.Y);
                    var capSq = board.GetSquare(cap.X, cap.Y);
                    var toSq = board.GetSquare(to.X, to.Y);
                    Console.WriteLine($"    from IsEmpty={fromSq.IsEmpty} piece={(fromSq.IsEmpty ? "null" : fromSq.Piece!.Color.ToString())}");
                    Console.WriteLine($"    captured IsEmpty={capSq.IsEmpty} piece={(capSq.IsEmpty ? "null" : capSq.Piece!.Color.ToString())}");
                    Console.WriteLine($"    landing IsEmpty={toSq.IsEmpty}");
                }
            }
            else
            {
                Console.WriteLine("DEBUG: No captures detected.");
            }
//////////


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

                // Show only this piece’s valid moves
                int indexPosition = 1;
                foreach (var moveList in validMoves)
                {
                    Console.WriteLine($"{indexPosition}. ({moveList.x},{moveList.y})");
                    indexPosition++;
                }

                Console.Write("Enter a number: ");
                string? inputMove = Console.ReadLine();

                if (int.TryParse(inputMove, out int moveChoice) && moveChoice > 0 && moveChoice <= validMoves.Count)
                {
                    var chosenMove = validMoves[moveChoice - 1];

                    // Build Move object
                    var move = new Move(
                        new Position(selectedPiece.Position.X, selectedPiece.Position.Y),
                        new Position(chosenMove.x, chosenMove.y)
                    );

                    if (game.ValidateMove(move))
                    {
                        game.ApplyMove(move);
                        board.PrintBoard();
                        Console.WriteLine($"Moved from ({move.From.X},{move.From.Y}) to ({move.To.X},{move.To.Y})");
                    }
                    else
                    {
                        Console.WriteLine("Invalid move.");
                    }

                    // Console.WriteLine($"You chose to move from ({selectedPiece.Position.X},{selectedPiece.Position.Y}) " +
                    //                   $"to ({chosenMove.x},{chosenMove.y})");

                    // Example: actually apply the move
                    // game.ApplyMove(new Move(selectedPiece.Position, new Position(chosenMove.x, chosenMove.y)));
                }
                else
                {
                    Console.WriteLine("Invalid move selection.");
                }
            }
            else
            {
                Console.WriteLine("Invalid selection.");
            }

            

            if (input.Trim().Equals("exit", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Exiting game...");
                break;
            }
            
            // Create move
            // var move = new Move(
            //     new Position(fromX, fromY),
            //     new Position(toX, toY)
            // );
            // Split input safely (ignoring extra spaces)
            // var parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

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

            // int fromX = 1;
            // int fromY = 2;

            // int toX = 2;
            // int toY = 3;


            



            // Try applying move
            // try
            // {
            //     if (game.ValidateMove(move))
            //     {
            //         game.ApplyMove(move);
            //         // if (!game.IsGameOver(game.CurrentPlayer.Color))  
            //         board.PrintBoard();
            //         // {
            //         // game.SwitchTurn();
            //         // }
            //     }
            // }
            // catch (Exception ex)
            // {
            //     Console.WriteLine("Invalid move: " + ex.Message);
            //     Console.ReadKey();
            // }
        }
    }
}