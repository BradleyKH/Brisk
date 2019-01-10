using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Threading.Tasks;
using GeoCoordinatePortable;
using Newtonsoft.Json;
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
            return client.DownloadString(currentURL + "lat=" + latitude + "&lon=" + longtitude);
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

        public static CurrentReport GenerateCurrentReport(string result)
        {
            var r = new CurrentReport()
            {
                City = JObject.Parse(result)["name"].ToString(),
                Latitude = Convert.ToDouble(JObject.Parse(result)["coord"]["lat"]),
                Longtitude = Convert.ToDouble(JObject.Parse(result)["coord"]["lon"]),
                CurrentTemp = KToF(JObject.Parse(result)["main"]["temp"].ToString()),
                Humidity = Convert.ToInt32(JObject.Parse(result)["main"]["humidity"]),
                Icon = "http://openweathermap.org/img/w/" + JObject.Parse(result)["weather"][0]["icon"].ToString() + ".png",
                WeatherCode = Convert.ToInt32(JObject.Parse(result)["weather"][0]["id"].ToString()),
                Description = JObject.Parse(result)["weather"][0]["description"].ToString()
            };

            return r;
        }

        public static Forecast GenerateForecast(string result)
        {
            var rawResult = JsonConvert.DeserializeObject<dynamic>(result);
            var rawList = rawResult["list"];
            var forecast = new Forecast()
            {
                City = rawResult["city"]["name"].ToString(),
                Latitude = Convert.ToDouble(rawResult["city"]["coord"]["lat"]),
                Longtitude = Convert.ToDouble(rawResult["city"]["coord"]["lon"])
            };

            var reports = new List<ForecastReport>();
            
            for (int i = 0; i < 40; i++)
            {
                var r = new ForecastReport()
                {
                    CurrentTemp = KtoF(Convert.ToDouble(rawList[i]["main"]["temp"])),
                    Humidity = Convert.ToInt32(rawList[i]["main"]["humidity"]),
                    WeatherCode = Convert.ToInt32(rawList[i]["weather"][0]["id"]),
                    Icon = "http://openweathermap.org/img/w/" + rawList[i]["weather"][0]["icon"].ToString() + ".png",
                    Description = rawList[i]["weather"][0]["description"].ToString(),
                    Time = rawList[i]["dt_txt"].ToString()
                };

                reports.Add(r);
            }

            forecast.Reports = reports;

            return forecast;
        }
    }
}
