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
using LicenseProject.Services;
using LicenseProject.Wrapper.Interfaces;

namespace LicenseProject.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly Context _context;
        private readonly IReviewService _review;
        private readonly IRestaurantService _restaurant;
        private readonly ITuristicObjectService _turisticObject;

        public ReviewsController(Context context , IReviewService review, IRestaurantService restaurant, ITuristicObjectService turisticObject)
        {
            _context = context;
            _review = review;
            _restaurant = restaurant;
            _turisticObject=turisticObject;
        }

        [HttpPost]
        public IActionResult AddReviewRestaurant(int restaurantId, int rate, string comment)
        {
            var currentUser = this.ControllerContext.HttpContext.User.Identity.Name;

            if (_context.ApplicationUsers.FirstOrDefault(x => x.UserName == currentUser) != null)
            {
                Review review = new Review()
                {
                    ReviewDescription = comment,
                    Date = DateTime.Now,
                    Rate = rate,
                    Restaurant = _context.Restaurants.FirstOrDefault(r=>r.RestaurantId== restaurantId),
                    ApplicationUser = _context.ApplicationUsers.FirstOrDefault(x => x.UserName == currentUser)
                };
                _review.Create(review);
            
            }
            return RedirectToAction("RestaurantInfo","Restaurants", new { Id = restaurantId });
        }

        [HttpPost]
        public IActionResult AddReviewTO(int TOId, int rate, string comment)
        {
            var currentUser = this.ControllerContext.HttpContext.User.Identity.Name;

            if (_context.ApplicationUsers.FirstOrDefault(x => x.UserName == currentUser) != null)
            {
                Review review = new Review()
                {
                    ReviewDescription = comment,
                    Date = DateTime.Now,
                    Rate = rate,
                    TuristicObject = _context.TuristicObjects.FirstOrDefault(r => r.TuristicObjectId == TOId),
                    ApplicationUser = _context.ApplicationUsers.FirstOrDefault(x => x.UserName == currentUser)
                };
                _review.Create(review);

            }
            return RedirectToAction("TuristicObjectInfo", "TuristicObjects", new { Id = TOId });
        }

        private bool ReviewExists(int id)
        {
            if (_review.GetReviewById(id) != null)
                return true;
            else return false;
        }
    }
}
