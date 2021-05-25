using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LicenseProject.Models
{
    public class FavoriteListRestaurant
    {
        public string FavoriteListRestaurantId { get; set; }
        public List<FavoriteRestaurant> FavoriteRestaurants { get; set; }
    }
}
