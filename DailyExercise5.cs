using System;
/*
Daily Task #5: Motivational Quote Generator
Objective: Create a simple console app that displays a random motivational quote each time it runs.

Today I was so tired that i did something small & stupid sorry T.T
 */

namespace DailyExercise5
{
    class Program
    {
        static void Main()
        {
            // 1. Create list of motivational quotes
            string[] quotes =
            {
                "Code is like humor. When you have to explain it, it's bad.",
                "The best error message is the one that never shows up.",
                "First, solve the problem. Then, write the code.",
                "Confusion is part of programming.",
                "When nothing goes right... go left!",
                "Programming isn't about what you know; it's about what you can figure out."
            };

            // 2. Generate random number
            Random randomGenerator = new Random();
            int randomIndex = randomGenerator.Next(quotes.Length);

            // 3. Display fancy output
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("==================================");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("    YOUR DAILY MOTIVATION         ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("==================================");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine($"\"{quotes[randomIndex]}\"");
            Console.WriteLine();
            Console.WriteLine("==================================");
            Console.WriteLine($"        {DateTime.Now:d}         ");
            Console.WriteLine("==================================");
        }
    }
}