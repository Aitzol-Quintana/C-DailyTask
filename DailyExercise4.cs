/*
 
 📋 Daily Task #4: CSV File Processor
Objective:
Build a robust CSV processor that:

Reads a CSV file with format: Name,Age,Email

Validates:

File exists

All rows have 3 columns

Age is valid number (18-100)

Outputs valid emails to valid_users.txt

Handles these custom errors:

File not found

Invalid file format

Corrupted data rows

 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DailyExercise4
{
    // Custom exception
    public class InvalidFileFormatException : Exception
    {
        public InvalidFileFormatException(string message) : base(message) { }
    }

    class Program
    {
        
    
        static void Main(string[] args)
        {
            try
            {

                Console.Write("Enter CSV file path: ");
                string filePath = CleanFilePath(Console.ReadLine());

                

                // 1. Check file exists
                if (!File.Exists(filePath))
                {
                    throw new FileNotFoundException("CSV file not found");
                }

                // 2. Read and process file
                List<string> validEmails = new List<string>();
                string[] lines = File.ReadAllLines(filePath);

                for (int i = 0; i < lines.Length; i++)
                {
                    try
                    {
                        // Processing logic here
                        var columns = lines[i].Split(',');

                        // Column validation
                        if (columns.Length != 3)
                            throw new InvalidFileFormatException("Missing columnms");

                        // Age validation
                        if (!int.TryParse(columns[1], out int age))
                            throw new FormatException("Not valid Age");

                        if (age >= 18 && age <= 100)
                        {
                            validEmails.Add(columns[2].Trim('"')); // Delete quote marks
                        }
                    }
                    catch (InvalidFileFormatException ex)
                    {
                        Console.WriteLine($"Line {i + 1} error: {ex.Message}");
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine($"Line {i + 1}: Invalid age format");
                    }
                }

                // 3. Save valid emails
                File.WriteAllLines("valid_users.txt", validEmails);
                Console.WriteLine($"Success! Valid emails saved: {validEmails.Count}");
            }
            catch (Exception ex) when (
                ex is FileNotFoundException ||
                ex is DirectoryNotFoundException)
            {
                Console.WriteLine($"File error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Critical error: {ex.Message}");
            }
        }
        private static string CleanFilePath(string input)
        {
            // Step 1: Delete spaces and quotation marks
            string cleaned = input.Trim().Trim('"', '\'', ' ');

            // Step 2: Replace special characters
            cleaned = cleaned.Replace("´", "'").Replace("‘", "'").Replace("’", "'");

            // Step 3: Normalize path with ~
            if (cleaned.Contains("~"))
            {
                string userProfile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                cleaned = cleaned.Replace("~", userProfile);
            }

            // Step 4: Make it absolute path
            return Path.GetFullPath(cleaned);
        }
    }
}


