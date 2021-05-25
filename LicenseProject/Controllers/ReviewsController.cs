using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LicenseProject.Models;
using LicenseProject.ViewModels;
using System.Web;

namespace LicenseProject.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly Context _context;

        public ReviewsController(Context context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult AddReview(int restaurantId, int rate, string comment, Review Review)
        {
            var currentUser = this.ControllerContext.HttpContext.User.Identity.Name;

            if (_context.ApplicationUsers.FirstOrDefault(x => x.UserName == currentUser) != null)
            {

                Review review = new Review()
                {
                    ReviewDescription = comment,
                    ApplicationUser = _context.ApplicationUsers.FirstOrDefault(x => x.UserName == currentUser),
                    Rate = rate,
                    Date = DateTime.Now,
                    Restaurant = _context.Restaurants.Where(r => r.RestaurantId == restaurantId).First()
                };
                _context.Reviews.Add(review);
                _context.SaveChanges();
            }
            return RedirectToAction("RestaurantInfo","Restaurants", new { Id = restaurantId });
        }

        private bool ReviewExists(int id)
        {
            return _context.Reviews.Any(e => e.ReviewId == id);
        }
    }
}
