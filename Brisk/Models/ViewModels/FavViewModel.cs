using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Brisk.Models.ViewModels
{
    public class FavViewModel
    {
        public List<Location> Locations { get; set; }
        public List<CurrentReport> Reports { get; set; }
    }
}
