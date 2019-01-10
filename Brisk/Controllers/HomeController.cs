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

        public IActionResult Favorites()
        {
            var fvm = new FavViewModel()
            {
                Locations = LocationRepository.GetLocations()
            };
            
            var reports = new List<Report>();

            for (int i = 0; i < fvm.Locations.Count; i++)
            {
                var result = Helpers.GetWeatherByZip(fvm.Locations[i].Zip);
                var code = Convert.ToInt32(Helpers.Parse(result, "cod"));
                if (code == 200)
                {
                    var r = new Report()
                    {
                        City = Helpers.Parse(result, "name"),
                        CurrentTemp = Helpers.KToF(Helpers.Parse(result, "main", "temp")),
                        HighTemp = Helpers.KToF(Helpers.Parse(result, "main", "temp_max")),
                        LowTemp = Helpers.KToF(Helpers.Parse(result, "main", "temp_min")),
                        Humidity = Convert.ToInt32(Helpers.Parse(result, "main", "humidity")),
                        Description = Helpers.GetDescription(result)
                    };

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
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
