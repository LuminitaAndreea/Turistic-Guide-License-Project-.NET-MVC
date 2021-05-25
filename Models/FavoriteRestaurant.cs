using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LicenseProject.Models
{
    public class FavoriteRestaurant
    {
        public int FavoriteRestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }
    }
}
