using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Brisk.Models;
using Brisk.Models.ViewModels;

namespace Brisk.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Local(double latitude, double longtitude)
        {
            var svm = new SearchViewModel();
            var result = Helpers.GetWeatherByCoords(latitude, longtitude);
            var code = Convert.ToInt32(Helpers.Parse(result, "cod"));

            if (code == 200)
                svm.Current = Helpers.GenerateCurrentReport(result);

            return View(svm);
        }

        public IActionResult Search(int zip)
        {
            var svm = new SearchViewModel();
            var result = Helpers.GetWeatherByZip(zip);
            var code = Convert.ToInt32(Helpers.Parse(result, "cod"));
            if (code == 200)
                svm.Current = Helpers.GenerateCurrentReport(result);

            return View(svm);
        }

        public IActionResult Favorites()
        {
            var fvm = new FavViewModel()
            {
                Locations = LocationRepository.GetLocations()
            };
            
            var reports = new List<CurrentReport>();

            for (int i = 0; i < fvm.Locations.Count; i++)
            {
                var result = Helpers.GetWeatherByZip(fvm.Locations[i].Zip);
                var code = Convert.ToInt32(Helpers.Parse(result, "cod"));
                if (code == 200)
                {
                    var r = Helpers.GenerateCurrentReport(result);
                    reports.Add(r);
                }
            }

            fvm.Reports = reports;

            return View(fvm);
        }

        public IActionResult CreateFavorite(int zip, string name)
        {
            var l = new Location()
            {
                Creator = 1,
                Zip = zip,
                Name = name
            };

            LocationRepository.CreateLocation(l);
            return RedirectToAction("Favorites");
        }

        public IActionResult Forecast(double lat, double lon)
        {
            var fvm = new ForecastViewModel();
            var result = Helpers.GetForecastByCoords(lat, lon);
            var code = Convert.ToInt32(Helpers.Parse(result, "cod"));

            if (code == 200)
                fvm.FiveDay = Helpers.GenerateForecast(result);
            
            return View(fvm);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
