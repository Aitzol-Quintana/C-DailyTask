using System;
using System.Collections.Generic;


/*
 Objective: Create a console application that displays a 7-day weather forecast for Abanto-Zierbena using mock data.
Aitzol Quintana
 */

namespace SimpleWeatherApp
{
    public class DailyForecast
    {
        public string Day { get; set; }
        public int MinTemp { get; set; }
        public int MaxTemp { get; set; }
        public string WeatherCondition { get; set; }
        public int RainProbability { get; set; }
    }

    class DailyExercise10
    {
        static void Main(string[] args)
        {
            Console.WriteLine("7-Day Weather Forecast for Abanto-Zierbena");
            Console.WriteLine("==========================================");

            List<DailyForecast> weatherForecast = GenerateWeatherData();

            DisplayForecast(weatherForecast);
        }

        static List<DailyForecast> GenerateWeatherData()
        {
            return new List<DailyForecast>
            {
                new DailyForecast { Day = "Monday", MinTemp = 17, MaxTemp = 26, WeatherCondition = "Partly Cloudy", RainProbability = 20 },
                new DailyForecast { Day = "Tuesday", MinTemp = 18, MaxTemp = 28, WeatherCondition = "Sunny", RainProbability = 10 },
                new DailyForecast { Day = "Wednesday", MinTemp = 19, MaxTemp = 25, WeatherCondition = "Rain Showers", RainProbability = 60 },
                new DailyForecast { Day = "Thursday", MinTemp = 16, MaxTemp = 22, WeatherCondition = "Thunderstorms", RainProbability = 80 },
                new DailyForecast { Day = "Friday", MinTemp = 15, MaxTemp = 21, WeatherCondition = "Cloudy", RainProbability = 30 },
                new DailyForecast { Day = "Saturday", MinTemp = 16, MaxTemp = 23, WeatherCondition = "Sunny Intervals", RainProbability = 40 },
                new DailyForecast { Day = "Sunday", MinTemp = 18, MaxTemp = 25, WeatherCondition = "Clear Sky", RainProbability = 5 }
            };
        }

        static void DisplayForecast(List<DailyForecast> forecast)
        {
            Console.WriteLine("\n{0,-10} | {1,-4} | {2,-4} | {3,-15} | {4}%",
                "Day", "Min", "Max", "Condition", "Rain Chance");
            Console.WriteLine("--------------------------------------------------");

            foreach (var day in forecast)
            {
                Console.WriteLine("{0,-10} | {1,3}°C | {2,3}°C | {3,-15} | {4,3}%",
                    day.Day,
                    day.MinTemp,
                    day.MaxTemp,
                    day.WeatherCondition,
                    day.RainProbability);
            }

            Console.WriteLine("\nData Source: Meteorological Simulation");
        }
    }
}
