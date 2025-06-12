using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

/*
 Objective: Build a console application to track personal expenses with categories, date filtering, and data persistence to a CSV file.
Aitzol Quintana
 */
namespace ExpenseTracker
{
    public class Expense
    {
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public string Category { get; set; }

        public Expense(DateTime date, string description, decimal amount, string category)
        {
            Date = date;
            Description = description;
            Amount = amount;
            Category = category;
        }
    }

    public class ExpenseManager
    {
        private List<Expense> _expenses = new();
        private const string DataFile = "expenses.csv";

        public void AddExpense(Expense expense) => _expenses.Add(expense);

        public void LoadData()
        {
            if (!File.Exists(DataFile)) return;

            _expenses = File.ReadAllLines(DataFile)
                .Skip(1)
                .Select(line => line.Split(','))
                .Where(fields => fields.Length == 4)
                .Select(fields => new Expense(
                    DateTime.Parse(fields[0]),
                    fields[1],
                    decimal.Parse(fields[2]),
                    fields[3]))
                .ToList();
        }

        public void SaveData()
        {
            using var writer = new StreamWriter(DataFile);
            writer.WriteLine("Date,Description,Amount,Category");

            foreach (var expense in _expenses)
            {
                writer.WriteLine($"{expense.Date:yyyy-MM-dd},{expense.Description}," +
                                $"{expense.Amount},{expense.Category}");
            }
        }

        public List<Expense> GetExpenses(DateTime? startDate = null, DateTime? endDate = null)
        {
            return _expenses
                .Where(e =>
                    (!startDate.HasValue || e.Date >= startDate) &&
                    (!endDate.HasValue || e.Date <= endDate))
                .OrderByDescending(e => e.Date)
                .ToList();
        }

        public decimal GetTotalSpending(DateTime? startDate = null, DateTime? endDate = null)
        {
            return GetExpenses(startDate, endDate).Sum(e => e.Amount);
        }

        public Dictionary<string, decimal> GetCategoryBreakdown()
        {
            return _expenses
                .GroupBy(e => e.Category)
                .ToDictionary(
                    g => g.Key,
                    g => g.Sum(e => e.Amount)
                );
        }
    }

    class Dailytask13
    {
        static void Main()
        {
            var manager = new ExpenseManager();
            manager.LoadData();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("PERSONAL EXPENSE TRACKER");
                Console.WriteLine("========================");
                Console.WriteLine("1. Add New Expense");
                Console.WriteLine("2. View All Expenses");
                Console.WriteLine("3. View Spending Summary");
                Console.WriteLine("4. Filter by Date Range");
                Console.WriteLine("5. Exit");
                Console.Write("\nChoose an option: ");

                switch (Console.ReadLine())
                {
                    case "1": AddExpense(manager); break;
                    case "2": DisplayExpenses(manager); break;
                    case "3": DisplaySummary(manager); break;
                    case "4": FilterByDate(manager); break;
                    case "5": manager.SaveData(); return;
                    default: ShowMessage("Invalid option", ConsoleColor.Red); break;
                }
            }
        }

        static void AddExpense(ExpenseManager manager)
        {
            Console.Write("\nEnter date (yyyy-mm-dd): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime date))
                date = DateTime.Today;

            Console.Write("Description: ");
            var description = Console.ReadLine();

            Console.Write("Amount: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                ShowMessage("Invalid amount", ConsoleColor.Red);
                return;
            }

            Console.Write("Category: ");
            var category = Console.ReadLine();

            manager.AddExpense(new Expense(date, description, amount, category));
            ShowMessage("Expense added successfully!", ConsoleColor.Green);
        }

        static void DisplayExpenses(ExpenseManager manager)
        {
            var expenses = manager.GetExpenses();

            if (!expenses.Any())
            {
                ShowMessage("\nNo expenses found", ConsoleColor.Yellow);
                Console.ReadKey();
                return;
            }

            Console.WriteLine("\nDate       | Description          | Amount   | Category");
            Console.WriteLine("-------------------------------------------------------");

            foreach (var expense in expenses)
            {
                Console.WriteLine($"{expense.Date:yyyy-MM-dd} | " +
                                $"{expense.Description,-20} | " +
                                $"{expense.Amount,8:C} | " +
                                $"{expense.Category}");
            }

            Console.WriteLine($"\nTotal: {expenses.Sum(e => e.Amount):C}");
            Console.ReadKey();
        }

        static void DisplaySummary(ExpenseManager manager)
        {
            var breakdown = manager.GetCategoryBreakdown();

            if (!breakdown.Any())
            {
                ShowMessage("\nNo spending data available", ConsoleColor.Yellow);
                Console.ReadKey();
                return;
            }

            Console.WriteLine("\nCATEGORY BREAKDOWN");
            Console.WriteLine("------------------");

            foreach (var category in breakdown.OrderByDescending(kvp => kvp.Value))
            {
                Console.WriteLine($"{category.Key,-15}: {category.Value:C}");
            }

            Console.WriteLine($"\nTOTAL SPENDING: {breakdown.Sum(c => c.Value):C}");
            Console.ReadKey();
        }

        static void FilterByDate(ExpenseManager manager)
        {
            Console.Write("\nEnter start date (yyyy-mm-dd): ");
            var startInput = Console.ReadLine();

            Console.Write("Enter end date (yyyy-mm-dd): ");
            var endInput = Console.ReadLine();

            DateTime? startDate = string.IsNullOrWhiteSpace(startInput) ?
                null : DateTime.Parse(startInput);

            DateTime? endDate = string.IsNullOrWhiteSpace(endInput) ?
                null : DateTime.Parse(endInput);

            var expenses = manager.GetExpenses(startDate, endDate);
            var total = manager.GetTotalSpending(startDate, endDate);

            Console.WriteLine("\nFILTERED EXPENSES");
            Console.WriteLine($"Date Range: {startDate?.ToString("yyyy-MM-dd") ?? "Start"} to " +
                            $"{endDate?.ToString("yyyy-MM-dd") ?? "End"}");
            Console.WriteLine("--------------------------------------------");

            foreach (var expense in expenses)
            {
                Console.WriteLine($"{expense.Date:yyyy-MM-dd} - " +
                                $"{expense.Description}: " +
                                $"{expense.Amount:C}");
            }

            Console.WriteLine($"\nTOTAL: {total:C}");
            Console.ReadKey();
        }

        static void ShowMessage(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine($"\n{message}");
            Console.ResetColor();
            Console.ReadKey();
        }
    }
}