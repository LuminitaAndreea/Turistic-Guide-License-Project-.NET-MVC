using LicenseProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LicenseProject.ViewModels
{
    public class RestaurantList
    {
        public List<Restaurant> Restaurants { get; set; }
        public List<Category> Categories { get; set; }
        public string CurrentCategory { get; set; }
    }
}
