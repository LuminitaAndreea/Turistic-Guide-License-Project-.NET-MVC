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

namespace LicenseProject.Controllers
{
    public class TuristicObjectsController : Controller
    {
        private readonly Context _context;

        public TuristicObjectsController(Context context)
        {
            _context = context;
        }

        // GET: TuristicObjects
        public async Task<IActionResult> Index()
        {
            return View(await _context.TuristicObjects.ToListAsync());
        }

        // GET: TuristicObjects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var turisticObject = await _context.TuristicObjects
                .FirstOrDefaultAsync(m => m.TuristicObjectId == id);
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
        public async Task<IActionResult> Create([Bind("TuristicObjectId,Name,VisitingPrice,Url,TelephoneNumber,ExactLocation,Program,City,Country,Description,MainImage,Image1,Image2,Image3,Image4,Image5")] TuristicObject turisticObject)
        {
            if (ModelState.IsValid)
            {
                _context.Add(turisticObject);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(turisticObject);
        }

        // GET: TuristicObjects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var turisticObject = await _context.TuristicObjects.FindAsync(id);
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
        public async Task<IActionResult> Edit(int id, [Bind("TuristicObjectId,Name,VisitingPrice,Url,TelephoneNumber,ExactLocation,Program,City,Country,Description,MainImage,Image1,Image2,Image3,Image4,Image5")] TuristicObject turisticObject)
        {
            if (id != turisticObject.TuristicObjectId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(turisticObject);
                    await _context.SaveChangesAsync();
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
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var turisticObject = await _context.TuristicObjects
                .FirstOrDefaultAsync(m => m.TuristicObjectId == id);
            if (turisticObject == null)
            {
                return NotFound();
            }

            return View(turisticObject);
        }

        // POST: TuristicObjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var turisticObject = await _context.TuristicObjects.FindAsync(id);
            _context.TuristicObjects.Remove(turisticObject);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TuristicObjectExists(int id)
        {
            return _context.TuristicObjects.Any(e => e.TuristicObjectId == id);
        }
        public async Task<IActionResult> TuristicObjectInfo(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var TO = _context.TuristicObjects.FirstOrDefault(m => m.TuristicObjectId == id);

            if (TO == null)
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

            var reviews = _context.Reviews.Include(b => b.ApplicationUser).Where(r => r.TuristicObject.TuristicObjectId == id).ToList();
            var sum = reviews.Sum(r => r.Rate);
            int rate;
            if (reviews.Count() != 0)
                rate = reviews.Count() / sum;
            else
                rate = 0;
            var VM = new TOVM
            {
                MainImage = TO.MainImage,
                TuristicObjectId = TO.TuristicObjectId,
                Name = TO.Name,
                City = TO.City,
                Reviews = reviews,
                TuristicObjectsCategories = TO.TuristicObjectsCategories,
                AverageRating = rate

            };
            return View(VM);
        }
    }
}
