using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Brisk
{
    public class Report
    {
        public string City { get; set; }
        public int CurrentTemp { get; set; }
        public int HighTemp { get; set; }
        public int LowTemp { get; set; }
        public int Humidity { get; set; }
        public int WeatherCode { get; set; }
        public string Icon { get; set; }
        public string Description { get; set; }
    }
}
