using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Brisk.Models
{
    public class Forecast
    {
        public string City { get; set; }
        public double Latitude { get; set; }
        public double Longtitude { get; set; }
        public List<ForecastReport> Reports { get; set; }
    }
}
