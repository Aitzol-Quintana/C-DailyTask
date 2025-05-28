/*
 Today's Daily Task
Title: Notification System with Events.
Objective: Master C# events/delegates.
Duration: 45-60 mins.

Steps:

Create a User class with Email property.

Implement an OnRegistered event triggered when a user registers.

Subscribe a method that "sends" a simulated email (log to console).


 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjerciciosC_.EjerciciosDiarios
{
    public class DailyExercise1
    {
        static void Main()
        {
            User user1 = new User { Email = "aitzolq@gmail.com" };
            user1.OnRegistered += (Email) =>
            {
                Console.WriteLine($"Email sended to {Email}");
            };

            user1.Register();

        }



        public class User
        {
            public string Email { get; set; }
            public event Action<string> OnRegistered; // Event 

            public void Register()
            {
                if (string.IsNullOrEmpty(Email))
                {
                    throw new ArgumentException("The email is empty, please write a valid email");

                }
                else
                {
                    Console.WriteLine(Email + " registered succesfully");
                    OnRegistered?.Invoke(Email);
                }
            }
        }


    }
}
