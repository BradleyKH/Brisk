using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Brisk
{
    public class CurrentReport
    {
        public string City { get; set; }
        public double Latitude { get; set; }
        public double Longtitude { get; set; }
        public int CurrentTemp { get; set; }
        public int Humidity { get; set; }
        public int WeatherCode { get; set; }
        public string Icon { get; set; }
        public string Description { get; set; }
    }
}
