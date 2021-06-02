using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using LicenseProject.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity.Infrastructure;
using LicenseProject.ViewModels;
using LicenseProject.Wrapper.Interfaces;
using LicenseProject.Services;

namespace LicenseProject.Controllers
{
    public class TuristicObjectsController : Controller
    {
        private readonly Context _context;
        private readonly ITuristicObjectService _turisticObject;
        private readonly IProjectWrapper _wrapper;
        private readonly IReviewService _review;
        private readonly ICategoryService _category;
        public TuristicObjectsController(ITuristicObjectService turisticObject, IReviewService review, ICategoryService category, IProjectWrapper wrapper, Context context)
        {
            _context = context;
            _turisticObject = turisticObject;
            _wrapper = wrapper;
            _review = review;
            _category = category;
        }

        // GET: TuristicObjects
        public IActionResult Index()
        {
            return View( _turisticObject.Get());
        }

        // GET: TuristicObjects/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var turisticObject = _turisticObject.GetTuristicObjectById(id);
            if (turisticObject == null)
            {
                return NotFound();
            }

            return View(turisticObject);
        }

        // GET: TuristicObjects/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TuristicObjects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("TuristicObjectId,Name,VisitingPrice,Url,TelephoneNumber,ExactLocation,Program,City,Country,Description,MainImage,Image1,Image2,Image3,Image4,Image5")] TuristicObject turisticObject)
        {
            if (ModelState.IsValid)
            {
                _turisticObject.Create(turisticObject);
                return RedirectToAction(nameof(Index));
            }
            return View(turisticObject);
        }

        // GET: TuristicObjects/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var turisticObject = _turisticObject.GetTuristicObjectById(id);
            if (turisticObject == null)
            {
                return NotFound();
            }
            return View(turisticObject);
        }

        // POST: TuristicObjects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("TuristicObjectId,Name,VisitingPrice,Url,TelephoneNumber,ExactLocation,Program,City,Country,Description,MainImage,Image1,Image2,Image3,Image4,Image5")] TuristicObject turisticObject)
        {
            if (id != turisticObject.TuristicObjectId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _turisticObject.Update(turisticObject);
                }
                catch (Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException)
                {
                    if (!TuristicObjectExists(turisticObject.TuristicObjectId))
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
            return View(turisticObject);
        }

        // GET: TuristicObjects/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var turisticObject = _turisticObject.GetTuristicObjectById(id);
            if (turisticObject == null)
            {
                return NotFound();
            }

            return View(turisticObject);
        }

        // POST: TuristicObjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var turisticObject = _turisticObject.GetTuristicObjectById(id);
            _turisticObject.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool TuristicObjectExists(int id)
        {
            if (_turisticObject.GetTuristicObjectById(id) != null)
                return true;
            else return false;
        }
        public IActionResult TuristicObjectInfo(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var turisticObject = _turisticObject.GetTuristicObjectById(id);

            if (turisticObject == null)
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
            
            var reviews = _review.GetAllTuristicObjects().Where(r => r.TuristicObject.TuristicObjectId == id && r.TuristicObject!=null).ToList();
            var rate = reviews.Sum(r => r.Rate) / reviews.Count();
            turisticObject.AverageRating = rate;

            _turisticObject.Update(turisticObject);
            var VM = new TOVM
            {
                MainImage = turisticObject.MainImage,
                TuristicObjectId = turisticObject.TuristicObjectId,
                Name = turisticObject.Name,
                City = turisticObject.City,
                Reviews = reviews,
                TuristicObjectsCategories = turisticObject.TuristicObjectsCategories,
                AverageRating = turisticObject.AverageRating

            };
            return View(VM);
        }
        public IActionResult List(int id,CityVM City)
        {
            List<Category> categories;
            List<TuristicObject> turisticObjects;
            string currentCategory = string.Empty;
            var city = City.CityName;
            int category = id;
            categories = _category.Get().OrderBy(n => n.CategoryId).ToList();
            if (id == 0)
            {
                turisticObjects = _turisticObject.Get().OrderBy(n => n.TuristicObjectId).Where(r => r.City == city).ToList();
                currentCategory = "All activities";
            }
            else
            {
                if (category == 1)
                {
                    turisticObjects = _turisticObject.Get().Where(p => p.TuristicObjectsCategories.FirstOrDefault(c => c.CategoryId == 2).CategoryId == 2).Where(r => r.City == city).ToList();
                    currentCategory = "Outdoor ";
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
                    turisticObjects = _turisticObject.Get().Where(p => p.TuristicObjectsCategories.FirstOrDefault(c => c.CategoryId == 3).CategoryId == 3).Where(r => r.City == city).ToList();
                    currentCategory = "Cultural";
                }

            }

            var turisticObjectList = new TuristicObjectList()
            {
                TuristicObjects= turisticObjects,
                CurrentCategory = currentCategory,
                Categories = categories
            };

            return View(turisticObjectList);
        }
        [HttpPost]
        public RedirectToActionResult SearchByCity(CityVM City)
        {
            return RedirectToAction("List", "TuristicObjects", City);
        }
        [HttpGet]
        public ActionResult SearchByCity()
        {
            return View(new CityVM());
        }
    }
}

