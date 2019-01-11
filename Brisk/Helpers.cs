using System;
using System.Collections.Generic;
using System.Net;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Brisk.Models;

namespace Brisk
{
    public class Helpers
    {
        private static string currentURL = "http://api.openweathermap.org/data/2.5/weather?";
        private static string forecastURL = "http://api.openweathermap.org/data/2.5/forecast?";
        private static string api = Environment.GetEnvironmentVariable("WEATHER_API");

        public static string GetWeatherByZip(int zipcode)
        {
            var client = new WebClient();
            return client.DownloadString(currentURL + "zip=" + zipcode + api);
        }

        public static string GetWeatherByCoords(double latitude, double longtitude)
        {
            var client = new WebClient();
            return client.DownloadString(currentURL + "lat=" + latitude + "&lon=" + longtitude + api);
        }

        public static string GetForecastByZip(int zipcode)
        {
            var client = new WebClient();
            return client.DownloadString(forecastURL + "zip=" + zipcode + api);
        }

        public static string GetForecastByCoords(double latitude, double longtitude)
        {
            var client = new WebClient();
            return client.DownloadString(forecastURL + "lat=" + latitude + "&lon=" + longtitude + api);
        }

        public static int KToF(string kelvin)
        {
            return (int)Math.Round((Double.Parse(kelvin) - 273.15) * 9 / 5) + 32;
        }

        public static int KtoF(double kelvin)
        {
            return (int)Math.Round((kelvin - 273.15) * 9 / 5) + 32;
        }

        public static string Parse(string input, string key)
        {
            return JObject.Parse(input)[key].ToString();
        }

        public static CurrentReport GenerateCurrentReport(string input)
        {
            var result = JsonConvert.DeserializeObject<dynamic>(input);
            var r = new CurrentReport()
            {
                City = result["name"].ToString(),
                Latitude = Convert.ToDouble(result["coord"]["lat"]),
                Longtitude = Convert.ToDouble(result["coord"]["lon"]),
                CurrentTemp = KToF(result["main"]["temp"].ToString()),
                Humidity = Convert.ToInt32(result["main"]["humidity"]),
                Icon = "http://openweathermap.org/img/w/" + result["weather"][0]["icon"].ToString() + ".png",
                WeatherCode = Convert.ToInt32(result["weather"][0]["id"].ToString()),
                Description = result["weather"][0]["description"].ToString()
            };

            return r;
        }

        public static Forecast GenerateForecast(string input)
        {
            var result = JsonConvert.DeserializeObject<dynamic>(input);
            var list = result["list"];
            var forecast = new Forecast()
            {
                City = result["city"]["name"].ToString(),
                Latitude = Convert.ToDouble(result["city"]["coord"]["lat"]),
                Longtitude = Convert.ToDouble(result["city"]["coord"]["lon"])
            };

            var reports = new List<ForecastReport>();
            
            for (int i = 0; i < 39; i++)
            {
                DateTime dtUTC = DateTime.SpecifyKind(DateTime.Parse(list[i]["dt_txt"].ToString()), DateTimeKind.Utc);

                var r = new ForecastReport()
                {
                    CurrentTemp = KtoF(Convert.ToDouble(list[i]["main"]["temp"])),
                    Humidity = Convert.ToInt32(list[i]["main"]["humidity"]),
                    WeatherCode = Convert.ToInt32(list[i]["weather"][0]["id"]),
                    Icon = "http://openweathermap.org/img/w/" + list[i]["weather"][0]["icon"].ToString() + ".png",
                    Description = list[i]["weather"][0]["description"].ToString(),
                    DateTime = dtUTC.ToLocalTime()
                };

                reports.Add(r);
            }

            forecast.Reports = reports;

            return forecast;
        }
    }
}
