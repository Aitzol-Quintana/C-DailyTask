/*
Daily Task #2: Data Analysis System with LINQ
Objective: Master LINQ queries to process collections of objects.

Task Description:

Create a Product class with properties:

Id (int)

Name (string)

Category (string)

Price (decimal)

In the Main method:

Create a list of 15 products (mix categories: Electronics, Clothing, Food)

Use LINQ method syntax (not SQL-like syntax) to solve:
a. Electronics products priced > €100
b. Average price per category
c. Most expensive product in each category
d. Product names sorted alphabetically

Display results in a formatted console output.

Technical Requirements:
❗ Use LINQ methods exclusively
❗ Implement in a single console application
❗ Forbid for/foreach loops for data processing (only for displaying results)


 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;


namespace EjerciciosC_.EjerciciosDiarios
{
    public class DailyExercise2
    {
        // Step 1: Create class Product
        public class Product
        {

            public int Id { get; set; }
            public string Name { get; set; }
            public string Category { get; set; }

            public decimal Price { get; set; }


        }
        // Step 2: Create Main 
        public static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            char euro = (char)128;
            List<Product> Productlist = new List<Product>();
            Productlist.Add(new Product { Id = 1, Name = "TV", Category = "Electronics", Price = Convert.ToDecimal(1299.99) });
            Productlist.Add(new Product { Id = 2, Name = "Nike Sneakers", Category = "Clothes", Price = Convert.ToDecimal(299.99) });
            Productlist.Add(new Product { Id = 3, Name = "Apple", Category = "Food", Price = Convert.ToDecimal(1.99) });
            Productlist.Add(new Product { Id = 4, Name = "Apple Charger", Category = "Electronics", Price = Convert.ToDecimal(29.99) });
            Productlist.Add(new Product { Id = 5, Name = "Basic T-Shirt", Category = "Clothes", Price = Convert.ToDecimal(19.99) });
            Productlist.Add(new Product { Id = 6, Name = "Meat", Category = "Food", Price = Convert.ToDecimal(9.99) });
            Productlist.Add(new Product { Id = 7, Name = "Laptop", Category = "Electronics", Price = Convert.ToDecimal(499.99) });
            Productlist.Add(new Product { Id = 8, Name = "Levi's Jeans", Category = "Clothes", Price = Convert.ToDecimal(99.99) });
            Productlist.Add(new Product { Id = 9, Name = "Octopus", Category = "Food", Price = Convert.ToDecimal(7.99) });
            Productlist.Add(new Product { Id = 10, Name = "Hard Drive 4TB", Category = "Electronics", Price = Convert.ToDecimal(129.99) });
            Productlist.Add(new Product { Id = 11, Name = "Potatoes", Category = "Food", Price = Convert.ToDecimal(9.99) });
            Productlist.Add(new Product { Id = 12, Name = "Socks", Category = "Clothes", Price = Convert.ToDecimal(9.99) });
            Productlist.Add(new Product { Id = 13, Name = "Webcam", Category = "Electronics", Price = Convert.ToDecimal(29.99) });
            Productlist.Add(new Product { Id = 14, Name = "Adidas Jumper", Category = "Electronics", Price = Convert.ToDecimal(69.99) });
            Productlist.Add(new Product { Id = 15, Name = "Bread", Category = "Electronics", Price = Convert.ToDecimal(1.99) });

            var ElectronicsOver100 = Productlist.Where(c => c.Category == "Electronics" && c.Price > Convert.ToDecimal(100)).ToList();
            var AveragePerCategory = Productlist.GroupBy(c => c.Category).Select(g => new { Category = g.Key, average = Convert.ToDecimal(g.Average(p => p.Price)) }).ToList();
            var MostExpensivePerCategory = Productlist.GroupBy(p => p.Category).Select(g => new {Category = g.Key, MostExpensiveProduct = g.OrderByDescending(p => p.Price).First()}).ToList();
            var OrderByName = Productlist.Select(n => n.Name).OrderBy(name => name).ToList();

            // Step 3: Show Results
            Console.WriteLine("=== Electronics > 100\u20AC  ===");
            foreach (var p in ElectronicsOver100)
           {
                Console.WriteLine($"- {p.Name}: {p.Price} \u20AC ");
            }
            Console.WriteLine("");
            Console.WriteLine("=== Average price per category ===");
            foreach (var p in AveragePerCategory)
            {
                Console.WriteLine($"- {p.Category}: {p.average} \u20AC");
            }
            Console.WriteLine("");
            Console.WriteLine("=== Most expensive per category ===");
            foreach (var item in MostExpensivePerCategory)
            {
                Console.WriteLine($"- {item.Category}: {item.MostExpensiveProduct.Name} ({item.MostExpensiveProduct.Price})\u20AC");
            }
            Console.WriteLine("");
            Console.WriteLine("=== Ordered by name ===");
            foreach (var p in OrderByName)
            {
                Console.WriteLine($"- {p}");
            }
        }



       


    }
}
