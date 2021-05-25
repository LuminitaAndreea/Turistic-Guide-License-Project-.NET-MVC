using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LicenseProject.Models
{
    public class Restaurant
    {
        public int RestaurantId { get; set; }
        public string Name { get; set; }
        public string PriceRange { get; set; }
        public string Program { get; set; }
        public string Url { get; set; }
        public string TelephoneNumber { get; set; }
        public string ExactLocation { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string MainImage { get; set; }
        public string Image1 { get; set; }
        public string Image2 { get; set; }
        public string Image3 { get; set; }
        public string Image4 { get; set; }
        public string Image5 { get; set; }

        public ICollection<Review> Reviews { get; set; }
        public ICollection<RestaurantCategory> RestaurantsCategories { get; set; }
    }
}
