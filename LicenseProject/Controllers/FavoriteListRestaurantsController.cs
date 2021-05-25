using LicenseProject.Models;
using LicenseProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LicenseProject.Controllers
{
    public class FavoriteListRestaurantsController : Controller
    {
        private readonly Context _context;

        public FavoriteListRestaurantsController(Context context)
        {
            _context = context;
        }

        public ViewResult Index()
        {
            var currentUser = this.ControllerContext.HttpContext.User.Identity.Name;
            var id = _context.ApplicationUsers.FirstOrDefault(u => u.UserName == currentUser).Id;
            var items =  _context.FavoriteRestaurants.Where(c => c.ApplicationUser.Id == id)
                           .Include(s => s.Restaurant)
                           .ToList();
            foreach (var item in items)
                _context.FavoriteRestaurants.Add(item); 

            var fcvm = new FavoriteCartRestaurantsViewModel
            {
                FavoriteRestaurants = items

            };
            return View(fcvm);
        }

        public RedirectToActionResult AddToFavoriteCart(int restaurantId)
        {
            if (this.ControllerContext.HttpContext.User.Identity.Name != null)
            {
                var currentUser = this.ControllerContext.HttpContext.User.Identity.Name;
                var id = _context.ApplicationUsers.FirstOrDefault(u => u.UserName == currentUser).Id;
                var favoriteRestaurant = _context.FavoriteRestaurants.SingleOrDefault(
                    s => s.Restaurant.RestaurantId == restaurantId && s.ApplicationUser.Id == id);
                if (favoriteRestaurant == null)
                {
                    favoriteRestaurant = new FavoriteRestaurant
                    {
                        Restaurant = _context.Restaurants.FirstOrDefault(r => r.RestaurantId == restaurantId),
                        ApplicationUser = _context.ApplicationUsers.FirstOrDefault(u => u.UserName == currentUser)
                    };

                    _context.FavoriteRestaurants.Add(favoriteRestaurant);
                }
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Page/Account/Manage/Login", new { area = "Identity" });
            }
        }
        public RedirectToActionResult RemoveFromFavoriteCart(int restaurantId)
        {
            var currentUser = this.ControllerContext.HttpContext.User.Identity.Name;
            var id = _context.ApplicationUsers.FirstOrDefault(u => u.UserName == currentUser).Id;
            var favoriteRestaurant = _context.FavoriteRestaurants.SingleOrDefault(
                s => s.Restaurant.RestaurantId == restaurantId && s.ApplicationUser.Id == id);
            if (favoriteRestaurant != null)
            {
                _context.FavoriteRestaurants.Remove(favoriteRestaurant);
            }
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}

