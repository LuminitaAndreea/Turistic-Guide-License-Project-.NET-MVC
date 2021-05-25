using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LicenseProject.Models
{
    public class RestaurantCategory
    {
        [Key] public int RestaurantCId { get; set; }
        public int CategoryId { get; set; }
        public int RestaurantId { get; set; }

        public Restaurant Restaurant { get; set; }
        public Category Category { get; set; }
    }
}
