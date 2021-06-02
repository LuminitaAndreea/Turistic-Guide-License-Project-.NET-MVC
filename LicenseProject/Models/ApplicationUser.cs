using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LicenseProject.Models
{
    public class ApplicationUser : IdentityUser<int> 
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public DateTime BirthDate { get; set; }
        public List<FavoriteRestaurant>FavoriteRestaurants { get; set; }
        public List<FavoriteTuristicObject> FavoriteTuristicObjects { get; set; }
        public List<Review> Reviews { get; set; }
       
    }
    
    
}
