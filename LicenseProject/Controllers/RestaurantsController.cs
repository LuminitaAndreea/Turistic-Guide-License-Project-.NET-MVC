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

namespace LicenseProject.Controllers
{
    public class RestaurantsController : Controller
    {
        private readonly Context _context;

        public RestaurantsController(Context context)
        {
            _context = context;
        }

        // GET: Restaurants
        public async Task<IActionResult> Index()
        {
            return View(await _context.Restaurants.ToListAsync());
        }

        // GET: Restaurants/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurant = await _context.Restaurants
                .FirstOrDefaultAsync(m => m.RestaurantId == id);
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
        public async Task<IActionResult> Create([Bind("RestaurantId,Name,PriceRange,Program,Url,TelephoneNumber,ExactLocation,City,Country,MainImage,Image1,Image2,Image3,Image4,Image5")] Restaurant restaurant)
        {
            if (ModelState.IsValid)
            {
                _context.Add(restaurant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(restaurant);
        }

        // GET: Restaurants/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurant = await _context.Restaurants.FindAsync(id);
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
        public async Task<IActionResult> Edit(int id, [Bind("RestaurantId,Name,PriceRange,Program,Url,TelephoneNumber,ExactLocation,City,Country,MainImage,Image1,Image2,Image3,Image4,Image5")] Restaurant restaurant)
        {
            if (id != restaurant.RestaurantId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(restaurant);
                    await _context.SaveChangesAsync();
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
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurant = await _context.Restaurants
                .FirstOrDefaultAsync(m => m.RestaurantId == id);
            if (restaurant == null)
            {
                return NotFound();
            }

            return View(restaurant);
        }

        // POST: Restaurants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var restaurant = await _context.Restaurants.FindAsync(id);
            _context.Restaurants.Remove(restaurant);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RestaurantExists(int? id)
        {
            return _context.Restaurants.Any(e => e.RestaurantId == id);
        }

        public async Task<IActionResult> RestaurantInfo(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var restaurant = _context.Restaurants.FirstOrDefault(m => m.RestaurantId == id);

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

            var reviews = _context.Reviews.Include(b => b.ApplicationUser).Where(r=>r.Restaurant.RestaurantId==id).ToList();
            var sum = reviews.Sum(r => r.Rate);
            int rate;
            if (reviews.Count() != 0)
                rate = reviews.Count() / sum;
            else
                rate = 0;
            var restaurantVM = new RestaurantVM
            {
                MainImage = restaurant.MainImage,
                RestaurantId = restaurant.RestaurantId,
                Name = restaurant.Name,
                City = restaurant.City,
                Reviews = reviews,
                RestaurantsCategories = restaurant.RestaurantsCategories,
                AverageRating = rate

             };
            return View(restaurantVM);
        }
        public IActionResult List(int id,CityVM City)
        {
            List<Category> categories;
            List<Restaurant> restaurants;
            string currentCategory = string.Empty;
            int _category = id;
            var city = City.CityName;
            categories = _context.Categories.OrderBy(n => n.CategoryId).ToList();
            if (id == 0)
            {
                restaurants = _context.Restaurants.OrderBy(n => n.RestaurantId).Where(r=>r.City==city).ToList();
                currentCategory = "All restaurants in "+city;
            }
            else
            {
                if (_category == 1)
                {
                    restaurants = _context.Restaurants.Where(p => p.RestaurantsCategories.FirstOrDefault(c=>c.CategoryId==1).CategoryId==1).Where(r => r.City == city).ToList();
                    currentCategory = "Bistro";
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
                    restaurants = _context.Restaurants.Where(p => p.RestaurantsCategories.Equals(2)).Where(r => r.City == city).ToList();
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