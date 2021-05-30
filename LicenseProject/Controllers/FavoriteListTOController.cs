using LicenseProject.Models;
using LicenseProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LicenseProject.Controllers
{
    public class FavoriteListTOController : Controller
    {
        private readonly Context _context;

        public FavoriteListTOController(Context context)
        {
            _context = context;
        }

        public ViewResult Index()
        {
            var currentUser = this.ControllerContext.HttpContext.User.Identity.Name;
            var id = _context.ApplicationUsers.FirstOrDefault(u => u.UserName == currentUser).Id;
            var items = _context.FavoriteTuristicObjects.Where(c => c.ApplicationUser.Id == id)
                           .Include(s => s.TuristicObject)
                           .ToList();
            foreach (var item in items)
                _context.FavoriteTuristicObjects.Add(item);

            var fcvm = new FavoriteCartTOViewModel
            {
                FavoriteTuristicObjects = items

            };
            return View(fcvm);
        }

        public RedirectToActionResult AddToFavoriteCart(int Id)
        {
            if (this.ControllerContext.HttpContext.User.Identity.Name != null)
            {
                var currentUser = this.ControllerContext.HttpContext.User.Identity.Name;
                var id = _context.ApplicationUsers.FirstOrDefault(u => u.UserName == currentUser).Id;
                var favoriteTuristicObject = _context.FavoriteTuristicObjects.SingleOrDefault(
                    s => s.TuristicObject.TuristicObjectId == Id && s.ApplicationUser.Id == id);
                if (favoriteTuristicObject == null)
                {
                    favoriteTuristicObject = new FavoriteTuristicObject
                    {
                        TuristicObject = _context.TuristicObjects.FirstOrDefault(r => r.TuristicObjectId == Id),
                        ApplicationUser = _context.ApplicationUsers.FirstOrDefault(u => u.UserName == currentUser)
                    };

                    _context.FavoriteTuristicObjects.Add(favoriteTuristicObject);
                }
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Page/Account/Manage/Login", new { area = "Identity" });
            }
        }
        public RedirectToActionResult RemoveFromFavoriteCart(int Id)
        {
            var currentUser = this.ControllerContext.HttpContext.User.Identity.Name;
            var id = _context.ApplicationUsers.FirstOrDefault(u => u.UserName == currentUser).Id;
            var favoriteTuristicObject = _context.FavoriteTuristicObjects.SingleOrDefault(
                s => s.TuristicObject.TuristicObjectId == Id && s.ApplicationUser.Id == id);
            if (favoriteTuristicObject != null)
            {
                _context.FavoriteTuristicObjects.Remove(favoriteTuristicObject);
            }
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}