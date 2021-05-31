using LicenseProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LicenseProject.ViewModels
{
    public class HomeRecommenderLists
    {
        public List<Restaurant> RestaurantsInCity { get; set; }
        public List<TuristicObject> ClosestTuristicObjects { get; set; }
        public List<TuristicObject> TopTuristicObjects { get; set; }
    }
}
