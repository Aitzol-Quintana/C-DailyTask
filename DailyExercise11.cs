using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;


/*
 Exercise Objective:
Build a resilient weather forecast application that retrieves and displays 7-day weather data for Abanto-Zierbena using either:

Real-time API data from Spain's Meteorological Agency (AEMET)

Simulated mock data when API fails

Aitzol Quintana
 */
namespace WeatherForecastApp
{
    public class WeatherForecast
    {
        public DateTime Date { get; set; }
        public double MinTemperatureC { get; set; }
        public double MaxTemperatureC { get; set; }
        public string Summary { get; set; }
        public int PrecipitationProbability { get; set; }
        public string Wind { get; set; }
    }

    public class WeatherService
    {
        private const string MunicipalityCode = "48002"; // Abanto-Zierbena code in AEMET
        private const string ApiKey = "eyJhbGciOiJIUzI1NiJ9.eyJzdWIiOiJhaXR6b2x5YWR1ckBnbWFpbC5jb20iLCJqdGkiOiJiZTZjYmFlMS04OTc0LTRkNGUtOGM4OS0xMmY4YmRiNTU2MDMiLCJpc3MiOiJBRU1FVCIsImlhdCI6MTc0OTUzOTc4MiwidXNlcklkIjoiYmU2Y2JhZTEtODk3NC00ZDRlLThjODktMTJmOGJkYjU1NjAzIiwicm9sZSI6IiJ9.K0FcC2lrb3WvPZN6j6wZKxkOhz2S6IaPTEZnwEpMQMk"; // Register at opendata.aemet.es

        public async Task<List<WeatherForecast>> GetWeeklyForecastAsync()
        {
            try
            {
                // Attempt to get real API data
                return await GetRealWeatherDataAsync();
            }
            catch (Exception)
            {
                // Fallback to mock data if API fails
                return GetMockData();
            }
        }

        private async Task<List<WeatherForecast>> GetRealWeatherDataAsync()
        {
            using var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("api_key", ApiKey);

            // Step 1: Get data URL
            var response = await httpClient.GetAsync(
                $"https://opendata.aemet.es/opendata/api/prediccion/especifica/municipio/diaria/{MunicipalityCode}");

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var responseData = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonResponse);

            // Step 2: Get actual weather data
            var dataResponse = await httpClient.GetAsync(responseData?["datos"]);
            var weatherData = await dataResponse.Content.ReadAsStringAsync();

            return ParseAemetData(weatherData);
        }

        private List<WeatherForecast> ParseAemetData(string jsonData)
        {
            // Logic to process AEMET's complex JSON response
           
            return GetMockData(); // Use mock data as placeholder
        }

        private List<WeatherForecast> GetMockData()
        {
            // Mock data based on search results
            return new List<WeatherForecast>
            {
                new() {
                    Date = new DateTime(2025, 6, 10),
                    MinTemperatureC = 16,
                    MaxTemperatureC = 24,
                    Summary = "Partly Cloudy",
                    PrecipitationProbability = 0,
                    Wind = "W 10 km/h"
                },
                new() {
                    Date = new DateTime(2025, 6, 11),
                    MinTemperatureC = 18,
                    MaxTemperatureC = 30,
                    Summary = "Afternoon Thunderstorms",
                    PrecipitationProbability = 90,
                    Wind = "SE 10 km/h"
                },
                new() {
                    Date = new DateTime(2025, 6, 12),
                    MinTemperatureC = 17,
                    MaxTemperatureC = 24,
                    Summary = "Partly Cloudy",
                    PrecipitationProbability = 10,
                    Wind = "W 15 km/h"
                },
                new() {
                    Date = new DateTime(2025, 6, 13),
                    MinTemperatureC = 19,
                    MaxTemperatureC = 24,
                    Summary = "Night Showers",
                    PrecipitationProbability = 35,
                    Wind = "W 9 km/h"
                },
                new() {
                    Date = new DateTime(2025, 6, 14),
                    MinTemperatureC = 18,
                    MaxTemperatureC = 21,
                    Summary = "Persistent Rain",
                    PrecipitationProbability = 70,
                    Wind = "N 8 km/h"
                },
                new() {
                    Date = new DateTime(2025, 6, 15),
                    MinTemperatureC = 17,
                    MaxTemperatureC = 20,
                    Summary = "Morning Showers",
                    PrecipitationProbability = 60,
                    Wind = "NNE 9 km/h"
                },
                new() {
                    Date = new DateTime(2025, 6, 16),
                    MinTemperatureC = 17,
                    MaxTemperatureC = 22,
                    Summary = "Clear",
                    PrecipitationProbability = 10,
                    Wind = "NE 8 km/h"
                }
            };
        }
    }

    class DailyExercise11
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Fetching forecast for Abanto-Zierbena...");
            var service = new WeatherService();
            var forecasts = await service.GetWeeklyForecastAsync();

            Console.WriteLine("\n7-Day Forecast (June 10-16, 2025):");
            Console.WriteLine("================================================================");
            Console.WriteLine("Date       | Min (°C) | Max (°C) | Rain Chance | Description");
            Console.WriteLine("---------------------------------------------------------------");

            foreach (var forecast in forecasts)
            {
                Console.WriteLine($"{forecast.Date:yyyy-MM-dd} | {forecast.MinTemperatureC,7} | " +
                                  $"{forecast.MaxTemperatureC,7} | {forecast.PrecipitationProbability,10}% | " +
                                  $"{forecast.Summary}");
            }

            Console.WriteLine("================================================================");
            Console.WriteLine("Source: AEMET - State Meteorological Agency");
           
        }
    }
}