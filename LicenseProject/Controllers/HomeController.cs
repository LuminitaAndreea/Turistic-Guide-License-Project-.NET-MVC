using LicenseProject.Models;
using LicenseProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace LicenseProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Context _context;

        public HomeController(ILogger<HomeController> logger,Context context)
        {
            _logger = logger;
            _context = context;
          

        }

        public IActionResult Index()
        {
            List<Restaurant> restaurantsInCity;
            List<TuristicObject> closestTuristicObjects;
            List<TuristicObject> topTuristicObjects;
            topTuristicObjects = _context.TuristicObjects.OrderBy(t => t.AverageRating).Take(5).ToList();
            if (_context.ApplicationUsers.FirstOrDefault(x => x.UserName == this.ControllerContext.HttpContext.User.Identity.Name) != null) { 
                var city = _context.ApplicationUsers.First(U => U.UserName == this.ControllerContext.HttpContext.User.Identity.Name).City;
                restaurantsInCity = _context.Restaurants.Where(R => R.City == city).OrderBy(r=>r.AverageRating).Take(5).ToList();
                closestTuristicObjects = _context.TuristicObjects.Where(R => R.City == city).Take(5).ToList();
                var list = new HomeRecommenderLists
                {
                    RestaurantsInCity = restaurantsInCity,
                    ClosestTuristicObjects = closestTuristicObjects,
                    TopTuristicObjects=topTuristicObjects
                };
                return View(list);
            }
           
            else {
                var list = new HomeRecommenderLists
                {
                    RestaurantsInCity = null,
                    ClosestTuristicObjects = null,
                    TopTuristicObjects = topTuristicObjects
                };
                return View(list);
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
