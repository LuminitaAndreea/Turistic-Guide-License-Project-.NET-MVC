using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LicenseProject.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Cart1Id { get; set; }
        public int Cart2Id { get; set; }
        public FavoriteListRestaurant FavoriteCart1 { get; set; }
        public FavoriteListTuristicObject FavoriteCart2 { get; set; }
        public List<Review> Reviews { get; set; }
    }
}
