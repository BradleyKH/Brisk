using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Brisk.Models
{
    public class Location
    {
        public int Id { get; set; }
        public int Creator { get; set; }
        public int Zip { get; set; }
        public string Name { get; set; }
    }
}
