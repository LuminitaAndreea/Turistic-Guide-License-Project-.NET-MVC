using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LicenseProject.Models;

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
                catch (DbUpdateConcurrencyException)
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
    }
}
