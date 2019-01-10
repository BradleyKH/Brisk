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
        private static string currentURL = "http://api.openweathermap.org/data/2.5/weather?";
        private static string forecastURL = "http://api.openweathermap.org/data/2.5/forecast?";
        private static string api = Environment.GetEnvironmentVariable("WEATHER_API");

        public static void GetLocation()
        {
            
        }

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
            return client.DownloadString(forecastURL + "lat=" + latitude + "&lon=" + longtitude);
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

        public static string GetIcon(string input)
        {
            return "http://openweathermap.org/img/w/" + JObject.Parse(input)["weather"][0]["icon"].ToString() + ".png";
        }

        public static int GetWeatherCode(string input)
        {
            return Convert.ToInt32(JObject.Parse(input)["weather"][0]["id"].ToString());
        }

        public static string Parse(string input, string keys)
        {
            var keyArr = keys.Split('-');

            if (keyArr.Length == 1)
                return JObject.Parse(input)[keyArr[0]].ToString();

            return "";
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
