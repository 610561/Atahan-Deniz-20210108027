using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
public class Weather
{
    public string Temperature { get; set; }
    public string Wind { get; set; }
    public string Description { get; set; }
    public Forecast[] Forecast { get; set; }
}
public class Forecast
{
    public string Day { get; set; }
    public string Temperature { get; set; }
    public string Wind { get; set; }
}
class Program
{
    static HttpClient client = new HttpClient();
    static async Task Main(string[] args)
    {
        string[] apiEndpoints = new string[] {
            "https://goweather.herokuapp.com/weather/istanbul",
            "https://goweather.herokuapp.com/weather/izmir",
            "https://goweather.herokuapp.com/weather/ankara"
        };
        foreach (var endpoint in apiEndpoints)
        {
            var data = await GetWeatherData(endpoint);
            DisplayWeather(data);
            Console.WriteLine();
        }
    }
    static async Task<Weather> GetWeatherData(string endpoint)
    {
        HttpResponseMessage response = await client.GetAsync(endpoint);
        if (response.IsSuccessStatusCode)
        {
            string json = await response.Content.ReadAsStringAsync();
            Weather data = JsonConvert.DeserializeObject<Weather>(json);
            return data;
        }
        else
        {
            throw new Exception("Failed to retrieve weather data");
        }
    }
    static void DisplayWeather(Weather weather)
    {
        Console.WriteLine($"Temperature: {weather.Temperature}");
        Console.WriteLine($"Wind: {weather.Wind}");
        Console.WriteLine($"Description: {weather.Description}");
        Console.WriteLine("Forecast:");
        foreach (var forecast in weather.Forecast)
        {
            Console.WriteLine($"Day: {forecast.Day}");
            Console.WriteLine($"Temperature: {forecast.Temperature}");
            Console.WriteLine($"Wind: {forecast.Wind}");
        }
    }
}