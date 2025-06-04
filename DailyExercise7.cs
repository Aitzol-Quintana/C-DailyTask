
/*
 Objective: Create a console app that generates random retro pixel art patterns. Simple, visually satisfying.
Aitzol Quintana
 */

using System;

class DailyExercise7
{
    static void Main()
    {
        Console.Title = "8-BIT PIXEL ART GENERATOR";
        Console.Clear();

        // Set console size for art canvas
        Console.SetWindowSize(60, 30);
        Console.SetBufferSize(60, 30);

        // Define pixel characters and colors
        char[] pixels = { '▓', '▒', '░', '▄', '▀', '■', '◘', '○' };
        ConsoleColor[] colors =
        {
            ConsoleColor.Red,
            ConsoleColor.Green,
            ConsoleColor.Blue,
            ConsoleColor.Yellow,
            ConsoleColor.Magenta,
            ConsoleColor.Cyan
        };

        // Create random generator
        Random artSeed = new Random();

        // Generate pixel grid
        for (int y = 2; y < 25; y++)
        {
            for (int x = 5; x < 55; x++)
            {
                // Add some random blank spaces
                if (artSeed.Next(100) > 20)
                {
                    Console.SetCursorPosition(x, y);

                    // Choose random pixel and color
                    char pixel = pixels[artSeed.Next(pixels.Length)];
                    Console.ForegroundColor = colors[artSeed.Next(colors.Length)];

                    // Occasionally add sparkles
                    if (artSeed.Next(100) > 95)
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.Write('*');
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Write(pixel);
                    }
                }
            }
        }

        // Add UI frame
        Console.SetCursorPosition(3, 1);
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("┌──────────────────────────────────────────┐");

        Console.SetCursorPosition(3, 26);
        Console.WriteLine("└──────────────────────────────────────────┘");

        // Add title and controls
        Console.SetCursorPosition(10, 0);
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("RETRO PIXEL ART GENERATOR");

        Console.SetCursorPosition(8, 27);
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine("Press R: New Art   |   S: Save   |   ESC: Exit");

        // Wait for user input
        while (true)
        {
            var key = Console.ReadKey(true);
            if (key.Key == ConsoleKey.R)
            {
                Console.Clear();
                Main(); // Regenerate
            }
            else if (key.Key == ConsoleKey.Escape)
            {
                break;
            }
            else if (key.Key == ConsoleKey.S)
            {
                Console.SetCursorPosition(20, 28);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Art saved to memory! (imaginary)");
            }
        }
    }
}
