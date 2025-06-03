using System;

/*
Daily Task #6: Retro Text Adventure Game
Objective: Create a nostalgic 80s-style text adventure with branching choices. Simple, fun, and no complex coding!

Aitzol Quintana Gonzalez
 */

namespace DailyExercise6
{
    class Program
    {
        static void Main()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("┌──────────────────────────────────┐");
            Console.WriteLine("│   CYBER FOREST ADVENTURE 1987    │");
            Console.WriteLine("│      (use arrow keys)            │");
            Console.WriteLine("└──────────────────────────────────┘");
            Console.ResetColor();

            Console.WriteLine("\nYou wake in a neon-lit forest. Rain patters on glowing leaves.");
            Console.WriteLine("Before you: two paths shimmer.");
            Console.Write("\n[1] Follow the blue humming lights");
            Console.Write("\n[2] Climb toward the red pulsing vines\n> ");

            string choice = Console.ReadLine();

            if (choice == "1")
            {
                Console.WriteLine("\nThe blue path cools your skin. You find:");
                Console.WriteLine("A FLOPPY DISK glows in a mossy nest.");
                Console.Write("\n[1] Insert in your wrist-port");
                Console.Write("\n[2] Pocket it for later\n> ");

                if (Console.ReadLine() == "1")
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\n>> SYSTEM ONLINE: 'Greetings traveler. Escape route mapped.'");
                    Console.WriteLine("<< SECRET TUNNEL UNLOCKED! YOU WIN!");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n!! VINES CRACKLE: 'No data = no passage.'");
                    Console.WriteLine("<< The forest consumes you. GAME OVER.");
                }
            }
            else
            {
                Console.WriteLine("\nRed vines warm your hands. At the cliff top:");
                Console.WriteLine("A GLASS TERMINAL shows 9-digit code.");
                Console.Write("\n[1] Try 19870924");
                Console.Write("\n[2] Enter random numbers\n> ");

                if (Console.ReadLine() == "1")
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\n>> TERMINAL HUMS: 'Birthdate accepted.'");
                    Console.WriteLine("<< TRANSPORT BEAM ACTIVATED! YOU WIN!");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n!! ALARM BLARES: 'INTRUDER DETECTED'");
                    Console.WriteLine("<< Security drones swarm. GAME OVER.");
                }
            }

            Console.ResetColor();
            Console.Write("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}
