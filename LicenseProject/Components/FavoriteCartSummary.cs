using LicenseProject.Models;
using LicenseProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LicenseProject.Components
{
    //public class FavoriteCartSummary : ViewComponent
    //{
    //    private readonly FavoriteListRestaurants _favoriteCart;
    //    public FavoriteCartSummary(FavoriteListRestaurants favoriteCart)
    //    {
    //        _favoriteCart = favoriteCart;
    //    }
    //    public IViewComponentResult Invoke()
    //    {
    //        var items = _favoriteCart.GetFavoriteRestaurants();
    //        _favoriteCart.FavoriteRestaurants = items;

    //        var favcartViewModel = new FavoriteCartRestaurantsViewModel
    //        {
    //            FavoriteListRestaurants = _favoriteCart
    //        };
    //        return View(favcartViewModel);
    //    }


    //}
}
