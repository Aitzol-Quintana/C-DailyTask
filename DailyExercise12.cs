using System;
using System.Collections.Generic;
using System.Linq;
/*
 Objective: Create a console-based task manager that allows users to add, complete, view, and delete tasks using object-oriented principles.

Aitzol Quintana
 */
namespace DailyTaskManager
{
    public class TaskItem
    {
        public int Id { get; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CreatedDate { get; }

        public TaskItem(int id, string description)
        {
            Id = id;
            Description = description;
            IsCompleted = false;
            CreatedDate = DateTime.Now;
        }
    }

    public class TaskManager
    {
        private readonly List<TaskItem> _tasks = new();
        private int _nextId = 1;

        public void AddTask(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Task description cannot be empty");

            _tasks.Add(new TaskItem(_nextId++, description));
        }

        public void CompleteTask(int taskId)
        {
            var task = _tasks.FirstOrDefault(t => t.Id == taskId);
            if (task == null)
                throw new KeyNotFoundException($"Task ID {taskId} not found");

            task.IsCompleted = true;
        }

        public void DeleteTask(int taskId)
        {
            var task = _tasks.FirstOrDefault(t => t.Id == taskId);
            if (task == null)
                throw new KeyNotFoundException($"Task ID {taskId} not found");

            _tasks.Remove(task);
        }

        public List<TaskItem> GetAllTasks() => _tasks.OrderBy(t => t.CreatedDate).ToList();

        public List<TaskItem> GetPendingTasks() =>
            _tasks.Where(t => !t.IsCompleted).OrderBy(t => t.CreatedDate).ToList();
    }

    class DailyTask12
    {
        static void Main()
        {
            var manager = new TaskManager();
            bool isRunning = true;

            while (isRunning)
            {
                Console.Clear();
                DisplayMenu();
                DisplayTasks(manager);

                var input = Console.ReadKey();
                switch (input.KeyChar)
                {
                    case '1': AddNewTask(manager); break;
                    case '2': CompleteTask(manager); break;
                    case '3': DeleteTask(manager); break;
                    case '4': isRunning = false; break;
                    default: ShowError("Invalid option"); break;
                }
            }
        }

        static void DisplayMenu()
        {
            Console.WriteLine("DAILY TASK MANAGER");
            Console.WriteLine("==================");
            Console.WriteLine("1. Add New Task");
            Console.WriteLine("2. Complete Task");
            Console.WriteLine("3. Delete Task");
            Console.WriteLine("4. Exit");
            Console.WriteLine("------------------");
        }

        static void DisplayTasks(TaskManager manager)
        {
            var tasks = manager.GetAllTasks();

            if (!tasks.Any())
            {
                Console.WriteLine("\nNo tasks available. Add your first task!");
                return;
            }

            Console.WriteLine("\nID | Status   | Description");
            Console.WriteLine("---------------------------");

            foreach (var task in tasks)
            {
                Console.WriteLine($"{task.Id,-2} | " +
                                  $"{(task.IsCompleted ? "DONE" : "PENDING"),-8} | " +
                                  $"{task.Description}");
            }
            Console.WriteLine();
        }

        static void AddNewTask(TaskManager manager)
        {
            Console.Write("\nEnter task description: ");
            var description = Console.ReadLine();

            try
            {
                manager.AddTask(description);
                ShowSuccess("Task added successfully!");
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
            }
        }

        static void CompleteTask(TaskManager manager)
        {
            Console.Write("Enter task ID to complete: ");
            if (int.TryParse(Console.ReadLine(), out int taskId))
            {
                try
                {
                    manager.CompleteTask(taskId);
                    ShowSuccess("Task marked as completed!");
                }
                catch (Exception ex)
                {
                    ShowError(ex.Message);
                }
            }
            else
            {
                ShowError("Invalid task ID");
            }
        }

        static void DeleteTask(TaskManager manager)
        {
            Console.Write("Enter task ID to delete: ");
            if (int.TryParse(Console.ReadLine(), out int taskId))
            {
                try
                {
                    manager.DeleteTask(taskId);
                    ShowSuccess("Task deleted successfully!");
                }
                catch (Exception ex)
                {
                    ShowError(ex.Message);
                }
            }
            else
            {
                ShowError("Invalid task ID");
            }
        }

        static void ShowError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\nError: {message}");
            Console.ResetColor();
            Console.ReadKey();
        }

        static void ShowSuccess(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n{message}");
            Console.ResetColor();
            Console.ReadKey();
        }
    }
}