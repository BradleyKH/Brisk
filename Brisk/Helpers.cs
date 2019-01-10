using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using GeoCoordinatePortable;

namespace Brisk
{
    public class Helpers
    {
        private static string currentURL = Environment.GetEnvironmentVariable("CURRENT_URL");
        private static string forecastURL = Environment.GetEnvironmentVariable("FORECAST_URL");
        private static string api = Environment.GetEnvironmentVariable("WEATHER_API");

        public static void GetLocation()
        {
            
        }

        public static string GetWeatherByZip(int zipcode)
        {
            var client = new WebClient();
            return client.DownloadString(currentURL + zipcode.ToString() + api);
        }

        public static int KToF(string kelvin)
        {
            var result = (int)Math.Round((Double.Parse(kelvin) - 273.15) * 9 / 5) + 32;

            return result;
        }

        public static int KtoF(double kelvin)
        {
            var result = (int)Math.Round((kelvin - 273.15) * 9 / 5) + 32;

            return result;
        }

        public static string GetDescription(string input)
        {
            return JObject.Parse(input)["weather"][0]["description"].ToString();
        }

        public static string Parse(string input, string key1)
        {
            return JObject.Parse(input)[key1].ToString();
        }

        public static string Parse(string input, string key1, string key2)
        {
            return JObject.Parse(input)[key1][key2].ToString();
        }

        public static string Parse(string input, string key1, string key2, string key3)
        {
            return JObject.Parse(input)[key1][key2][key3].ToString();
        }
    }
}
