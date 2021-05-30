using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LicenseProject.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public string Description { get; set; }
        public string CategoryImage { get; set; }
        public ICollection<Restaurant> Restaurants { get; set; }
        public virtual ICollection<TuristicObject> TuristicObjects { get; set; }
    }
}
