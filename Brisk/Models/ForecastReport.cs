using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Brisk.Models
{
    public class ForecastReport
    {
        public int CurrentTemp { get; set; }
        public int Humidity { get; set; }
        public int WeatherCode { get; set; }
        public string Icon { get; set; }
        public string Description { get; set; }
        public string Time { get; set; }
    }
}
