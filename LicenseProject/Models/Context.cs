using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LicenseProject.Models
{
    public class Context :  IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
    { 
        public Context(DbContextOptions<Context> options)
         : base(options)
        { }
        public DbSet<TuristicObject> TuristicObjects { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<RestaurantCategory> RestaurantsCategories { get; set; }
        public DbSet<TuristicObjectCategory> TuristicObjectCategories { get; set; }
        public DbSet<FavoriteTuristicObject> FavoriteTuristicObjects { get; set; }
        public DbSet<FavoriteRestaurant> FavoriteRestaurants { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    }
}
