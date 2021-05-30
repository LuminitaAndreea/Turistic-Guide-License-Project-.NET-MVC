using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LicenseProject.Models
{
    public class FavoriteRestaurant
    {
        [Key] public int FavoriteRestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
