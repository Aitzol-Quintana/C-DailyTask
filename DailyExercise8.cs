using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
/*
 🔒 Daily Task #8: Password Strength Analyzer
Objective: Create a utility that checks password strength in real-time with detailed feedback.

Aitzol Quintana
 */
class PDailyExercise8
{
    static void Main()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("┌──────────────────────────────────────────┐");
        Console.WriteLine("│        PASSWORD STRENGTH ANALYZER        │");
        Console.WriteLine("└──────────────────────────────────────────┘");
        Console.ResetColor();

        while (true)
        {
            Console.Write("\nEnter password (type 'exit' to quit): ");
            string password = ReadMaskedInput();

            if (password.ToLower() == "exit") break;
            if (string.IsNullOrEmpty(password)) continue;

            AnalyzePassword(password);
        }
    }

    static void AnalyzePassword(string password)
    {
        // 1. Basic metrics
        int length = password.Length;
        bool hasUpper = password.Any(char.IsUpper);
        bool hasLower = password.Any(char.IsLower);
        bool hasDigit = password.Any(char.IsDigit);
        bool hasSpecial = Regex.IsMatch(password, @"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");

        // 2. Strength calculation
        int strengthScore = 0;
        string feedback = "";

        // Length
        if (length >= 12) strengthScore += 3;
        else if (length >= 8) strengthScore += 2;
        else if (length >= 6) strengthScore += 1;
        else feedback += "✘ Too short\n";

        // Complexity
        if (hasUpper) strengthScore++;
        if (hasLower) strengthScore++;
        if (hasDigit) strengthScore++;
        if (hasSpecial) strengthScore++;

        // Common patterns check
        if (IsCommonPassword(password))
        {
            strengthScore = Math.Max(0, strengthScore - 2);
            feedback += "✘ Common password\n";
        }

        // Sequential check (e.g., 12345)
        if (HasSequences(password))
        {
            strengthScore--;
            feedback += "✘ Sequential pattern detected\n";
        }

        // 3. Strength rating
        string strengthLevel;
        ConsoleColor color;

        if (strengthScore >= 8)
        {
            strengthLevel = "VERY STRONG";
            color = ConsoleColor.Green;
            feedback += "✓ Excellent security";
        }
        else if (strengthScore >= 6)
        {
            strengthLevel = "STRONG";
            color = ConsoleColor.DarkGreen;
            feedback += "✓ Good security";
        }
        else if (strengthScore >= 4)
        {
            strengthLevel = "MEDIUM";
            color = ConsoleColor.Yellow;
            feedback += "→ Add special characters";
        }
        else
        {
            strengthLevel = "WEAK";
            color = ConsoleColor.Red;
            feedback += "✘ Unacceptable for security";
        }

        // 4. Display results
        Console.WriteLine("\n┌─────── ANALYSIS ───────┐");
        Console.WriteLine($"│ Length: {length} chars");
        Console.WriteLine($"│ Uppercase: {(hasUpper ? "✓" : "✘")}");
        Console.WriteLine($"│ Lowercase: {(hasLower ? "✓" : "✘")}");
        Console.WriteLine($"│ Digits: {(hasDigit ? "✓" : "✘")}");
        Console.WriteLine($"│ Special: {(hasSpecial ? "✓" : "✘")}");
        Console.WriteLine("├─────────────────────────┤");
        Console.Write("│ Strength: ");
        Console.ForegroundColor = color;
        Console.Write(strengthLevel.PadRight(13));
        Console.ResetColor();
        Console.WriteLine("│\n└─────────────────────────┘");

        Console.WriteLine("\n🔍 Recommendations:");
        Console.WriteLine(feedback);
    }

    static bool IsCommonPassword(string password)
    {
        List<string> commonPasswords = new List<string>
        {
            "password", "123456", "qwerty", "letmein", "admin",
            "welcome", "monkey", "sunshine", "password1"
        };
        return commonPasswords.Contains(password.ToLower());
    }

    static bool HasSequences(string password)
    {
        string[] sequences = { "123", "abc", "qwe", "asd", "zxcv" };
        string lowerPass = password.ToLower();
        return sequences.Any(seq => lowerPass.Contains(seq));
    }

    // Mask password input with asterisks
    static string ReadMaskedInput()
    {
        string input = "";
        ConsoleKeyInfo key;

        do
        {
            key = Console.ReadKey(true);

            if (key.Key != ConsoleKey.Enter &&
                key.Key != ConsoleKey.Backspace &&
                key.Key != ConsoleKey.Escape)
            {
                input += key.KeyChar;
                Console.Write('*');
            }
            else if (key.Key == ConsoleKey.Backspace && input.Length > 0)
            {
                input = input.Substring(0, input.Length - 1);
                Console.Write("\b \b");
            }
        } while (key.Key != ConsoleKey.Enter);

        Console.WriteLine();
        return input;
    }
}
