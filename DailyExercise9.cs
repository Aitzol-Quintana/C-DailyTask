
/*
 🌟 Daily Task #9: Virtual Pet Simulator
Objective: Create a console-based virtual pet with emotions, needs, and simple interactions.

Aitzol Quintana
 */



using System;
using System.Threading;

class VirtualPet
{
    // Pet properties
    public string Name { get; set; }
    public int Happiness { get; set; } = 50;
    public int Hunger { get; set; } = 30;
    public int Energy { get; set; } = 70;

    // Emotional states
    private readonly string[] happyFaces = { ":)", "^_^", "=D", "(-:" };
    private readonly string[] sadFaces = { ":(", ">_<", "T_T", ")-:" };

    public void DisplayStatus()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"\n  ╔══════════════════════════╗");
        Console.WriteLine($"  ║       {GetFace()} {Name,-8}        ║");
        Console.WriteLine($"  ╠══════════════════════════╣");
        Console.WriteLine($"  ║ Happiness: {ProgressBar(Happiness)} ║");
        Console.WriteLine($"  ║ Hunger:    {ProgressBar(Hunger, true)} ║");
        Console.WriteLine($"  ║ Energy:    {ProgressBar(Energy)} ║");
        Console.WriteLine($"  ╚══════════════════════════╝");
        Console.ResetColor();
    }

    private string GetFace()
    {
        if (Happiness > 60) return happyFaces[new Random().Next(happyFaces.Length)];
        if (Happiness < 40) return sadFaces[new Random().Next(sadFaces.Length)];
        return ":-|";
    }

    private string ProgressBar(int value, bool inverse = false)
    {
        int bars = value / 10;
        string bar = new string('█', bars);
        string empty = new string('░', 10 - bars);
        return inverse ? $"{empty}{bar} {value}%" : $"{bar}{empty} {value}%";
    }

    public void Feed()
    {
        Hunger = Math.Max(0, Hunger - 25);
        Happiness = Math.Min(100, Happiness + 5);
        Console.WriteLine("\n> You gave some virtual kibble. *munch munch*");
        Thread.Sleep(1500);
    }

    public void Play()
    {
        if (Energy < 20)
        {
            Console.WriteLine("\n> Zzz... too tired to play right now...");
            Thread.Sleep(1500);
            return;
        }

        Happiness = Math.Min(100, Happiness + 15);
        Energy = Math.Max(0, Energy - 20);
        Console.WriteLine("\n> You played fetch with a digital stick! *wags tail*");
        Thread.Sleep(1500);
    }

    public void Rest()
    {
        Energy = Math.Min(100, Energy + 30);
        Hunger = Math.Min(100, Hunger + 15);
        Console.WriteLine("\n> You tucked your pet in for a nap. Zzz...");
        Thread.Sleep(1500);
    }

    public void TimePass()
    {
        Hunger = Math.Min(100, Hunger + 8);
        Energy = Math.Max(0, Energy - 5);
        if (Hunger > 70) Happiness = Math.Max(0, Happiness - 10);
    }
}

class DailyExercise9
{
    static void Main()
    {
        Console.WriteLine("* Welcome to Virtual Pet Simulator ");
        Console.Write("\nName your new digital companion: ");
        string name = Console.ReadLine();

        VirtualPet pet = new VirtualPet { Name = name };

        while (true)
        {
            pet.TimePass();
            pet.DisplayStatus();

            Console.WriteLine("\nChoose an action:");
            Console.WriteLine("[1] Feed \n[2] Play \n[3] Rest \n[4] Check status \n[5] Exit");
            Console.Write("> ");

            switch (Console.ReadLine())
            {
                case "1": pet.Feed(); break;
                case "2": pet.Play(); break;
                case "3": pet.Rest(); break;
                case "4":
                    Console.WriteLine("\n> Checking pet...");
                    Thread.Sleep(1000);
                    break;
                case "5": return;
                default:
                    Console.WriteLine("\n> Invalid choice. Try again.");
                    Thread.Sleep(1000);
                    break;
            }
        }
    }
}
