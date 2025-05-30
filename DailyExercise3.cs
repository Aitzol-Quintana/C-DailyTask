using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
 📅 Daily Task #3: Employee Management System


Objective
Build a C# console application that models a corporate employee system using object-oriented principles (inheritance, interfaces) to calculate bonuses and total salaries for different roles.

Problem Statement
Your company needs a system to manage employee compensation. There are two types of employees:

Developers

Receive 15% base salary bonus

Have a primary programming language specialization

Managers

Receive 25% base salary bonus

Oversee a specific department

The application should:

Calculate total bonuses paid to all employees

Identify the highest-paid employee (base salary + bonus)

Display comprehensive payroll reports
*/


namespace EmployeeSystem
{
    public abstract class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal BaseSalary { get; set; }

        // Abstract property to be implemented in derived classes
        public abstract decimal TotalSalary { get; }
    }

    public interface IBonusCalculator
    {
        decimal CalculateBonus();
    }

    public class Developer : Employee, IBonusCalculator
    {
        public string PrimaryLanguage { get; set; }  

        public decimal CalculateBonus()
        {
            // 15% bonus for developers
            return BaseSalary * 0.15m;
        }

        public override decimal TotalSalary => BaseSalary + CalculateBonus();
    }

    public class Manager : Employee, IBonusCalculator
    {
        public string Department { get; set; }  

        public decimal CalculateBonus()
        {
            // 25% bonus for managers
            return BaseSalary * 0.25m;
        }

        public override decimal TotalSalary => BaseSalary + CalculateBonus();
    }

    class Program
    {
        static void Main()
        {

            Console.OutputEncoding = Encoding.UTF8;
            // Create employee list
            List<Employee> employees = new List<Employee>
            {
                new Developer { Id=1, Name="Eneko Ramirez", BaseSalary=4500m, PrimaryLanguage="Java" },
                new Developer { Id=2, Name="Aketza Etxeberria", BaseSalary=5200m, PrimaryLanguage="Python" },
                new Manager { Id=3, Name="Uxue Bosque", BaseSalary=7800m, Department="Marketing" },
                new Manager { Id=4, Name="Iker Urretxa", BaseSalary=6800m, Department="IT" },
                new Developer { Id=5, Name="Aitzol Quintana", BaseSalary=7500m, PrimaryLanguage="C#" }
            };

            // Calculate total bonuses
            decimal totalBonuses = employees
                .OfType<IBonusCalculator>()
                .Sum(emp => emp.CalculateBonus());

            // Find best paid employee
            Employee bestPaidEmployee = employees
                .OrderByDescending(emp => emp.TotalSalary)
                .First();

            // Display results
            Console.WriteLine("===== EMPLOYEE REPORT =====");
            Console.WriteLine($"Total Bonuses Paid: {totalBonuses:F2}");
            Console.WriteLine("\nHighest Paid Employee:");
            Console.WriteLine($"- Name: {bestPaidEmployee.Name}");

            if (bestPaidEmployee is Developer dev)
                Console.WriteLine($"- Role: Developer ({dev.PrimaryLanguage})");
            else if (bestPaidEmployee is Manager mgr)
                Console.WriteLine($"- Role: Manager ({mgr.Department})");

            Console.WriteLine($"- Base Salary: {bestPaidEmployee.BaseSalary:F2} \u20AC");
            Console.WriteLine($"- Total Salary: {bestPaidEmployee.TotalSalary:F2} \u20AC");

            Console.WriteLine("\n===== ALL EMPLOYEES =====");
            foreach (var emp in employees)
            {
                string role = emp is Developer ? "Developer" : "Manager";
                Console.WriteLine($"{emp.Id}: {emp.Name} ({role}) - {emp.TotalSalary:F2} \u20AC");
            }
        }
    }
}