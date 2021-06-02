using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LicenseProject.Models;
using LicenseProject.ViewModels;
using System.ComponentModel.Design.Serialization;
using LicenseProject.Services;
using LicenseProject.Wrapper.Interfaces;

namespace LicenseProject.Controllers
{
    public class RestaurantsController : Controller
    {
        private readonly Context _context;
        private readonly IRestaurantService _restaurant;
        private readonly IReviewService _review;
        private readonly ICategoryService _category;

        public RestaurantsController(IRestaurantService restaurant,IReviewService review ,ICategoryService category, Context context)
        {
            _restaurant = restaurant;
            _review = review;
            _category = category;
            _context = context;
        }

        // GET: Restaurants
        public IActionResult Index()
        {
            return View(_restaurant.Get());
        }

        // GET: Restaurants/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurant =  _restaurant.GetRestaurantById(id);
                
            if (restaurant == null)
            {
                return NotFound();
            }

            return View(restaurant);
        }

        // GET: Restaurants/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Restaurants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("RestaurantId,Name,PriceRange,Program,Url,TelephoneNumber,ExactLocation,City,Country,MainImage,Image1,Image2,Image3,Image4,Image5")] Restaurant restaurant)
        {
            if (ModelState.IsValid)
            {
                _restaurant.Create(restaurant);
                return RedirectToAction(nameof(Index));
            }
            return View(restaurant);
        }

        // GET: Restaurants/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurant = _restaurant.GetRestaurantById(id);
            if (restaurant == null)
            {
                return NotFound();
            }
            return View(restaurant);
        }

        // POST: Restaurants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("RestaurantId,Name,PriceRange,Program,Url,TelephoneNumber,ExactLocation,City,Country,MainImage,Image1,Image2,Image3,Image4,Image5")] Restaurant restaurant)
        {
            if (id != restaurant.RestaurantId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _restaurant.Update(restaurant);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RestaurantExists(restaurant.RestaurantId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(restaurant);
        }

        // GET: Restaurants/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurant = _restaurant.GetRestaurantById(id);
            if (restaurant == null)
            {
                return NotFound();
            }

            return View(restaurant);
        }

        // POST: Restaurants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var restaurant = _restaurant.GetRestaurantById(id);
            _restaurant.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool RestaurantExists(int? id)
        {
            if (_restaurant.GetRestaurantById(id)!=null)
                return true;
            else return false;
        }

        public IActionResult RestaurantInfo(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var restaurant = _restaurant.GetRestaurantById(id);

            if (restaurant == null)
            {
                return NotFound();
            }

            var currentUser = this.ControllerContext.HttpContext.User.Identity.Name;
            if (_context.ApplicationUsers.FirstOrDefault(x => x.UserName == currentUser) == null)
            {
                ViewBag.Message = "You must be logged in to leave a review!";
                ViewBag.AlertClass = "alert alert-success alert-dismissible";
                ViewBag.AlertVisibility = "";
            }

            var reviews = _review.GetAllRestaurants().Where(r => r.Restaurant.RestaurantId == id && r.Restaurant != null).ToList();
            if (reviews.Count > 0) { 

                var rate = reviews.Sum(r => r.Rate) / reviews.Count();
                restaurant.AverageRating = rate;
                _restaurant.Update(restaurant);
             }
            else
                restaurant.AverageRating = 0;

            var restaurantVM = new RestaurantVM
            {
                MainImage = restaurant.MainImage,
                RestaurantId = restaurant.RestaurantId,
                Name = restaurant.Name,
                City = restaurant.City,
                Reviews = reviews,
                RestaurantsCategories = restaurant.RestaurantsCategories,
                AverageRating = restaurant.AverageRating

            };
            return View(restaurantVM);
        }
        public IActionResult List(int id,CityVM City)
        {
            List<Category> categories;
            List<Restaurant> restaurants;
            string currentCategory = string.Empty;
            int category = id;
            var city = City.CityName;
            categories = _category.Get().OrderBy(n => n.CategoryId).ToList();
            if (id == 0)
            {
                restaurants = _restaurant.Get().OrderBy(n => n.RestaurantId).Where(r=>r.City==city).ToList();
                currentCategory = "All restaurants in "+city;
            }
            else
            {
                if (category == 1)
                {
                    restaurants = _restaurant.Get().Where(p => p.RestaurantsCategories.FirstOrDefault(c=>c.CategoryId==1).CategoryId==1).Where(r => r.City == city).ToList();
                    currentCategory = "Greek";
                }
                //else if (_category == 2)
                //{
                //    restaurants = _context.Restaurant.Where(p => p.Category.NameCategory.Equals("pizza"));
                //    currentCategory = "Pizza places";
                //}
                //else if (_category == 3)
                //{
                //    restaurants = _context.Restaurant.Where(p => p.Category.NameCategory.Equals("traditional"));
                //    currentCategory = "Traditional restaurants";
                //}

                //else if (_category == 4)
                //{
                //    restaurants = _context.Restaurant.Where(p => p.Category.NameCategory.Equals("fancy"));
                //    currentCategory = "Fancy restaurants";
                //}

                else
                {
                    restaurants = _restaurant.Get().Where(p => p.RestaurantsCategories.Equals(2)).Where(r => r.City == city).ToList();
                    currentCategory = "Cookies and Sweets";
                }

            }

            var listRestaurants = new RestaurantList
            {
                Restaurants = restaurants,
                CurrentCategory = currentCategory,
                Categories = categories
            };

            return View(listRestaurants);
        }
        [HttpPost]
        public RedirectToActionResult SearchByCity(CityVM City)
        {
            return RedirectToAction("List","Restaurants",City);
        }
        [HttpGet]
        public ActionResult SearchByCity()
        {
            return View(new CityVM());
        }

    }
}