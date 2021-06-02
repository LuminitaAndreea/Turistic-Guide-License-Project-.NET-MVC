using LicenseProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LicenseProject.ViewModels
{
    public class RecommendationsViewModel
    {
        public List<Restaurant> RestaurantRecommended { get; set; }
        public List<TuristicObject> TuristicObjectsRecommended { get; set; }

        public List<Restaurant> RestaurantsInCity { get; set; }
        public List<TuristicObject> ClosestTuristicObjects { get; set; }
        public List<TuristicObject> TopTuristicObjects { get; set; }

        public int UserId { get; set; }
    }
}
