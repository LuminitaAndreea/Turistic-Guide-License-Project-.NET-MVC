using LicenseProject.Models;
using LicenseProject.Services;
using LicenseProject.ViewModels;
using LicenseProject.Wrapper.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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
        private readonly IRestaurantService _restaurant;
        private readonly ITuristicObjectService _turisticObject;
        private readonly IProjectWrapper _wrapper;
        private readonly IRestaurantRecommenderService _recommenderRestaurants;
        private readonly ITuristicObjectRecommenderService _recommenderTO;

        public HomeController(ILogger<HomeController> logger,Context context,IRestaurantRecommenderService recommenderR,
            ITuristicObjectRecommenderService recommenderT,IRestaurantService restaurantService,ITuristicObjectService turisticObjectService,
            IProjectWrapper wrapper)
        {
            _logger = logger;
            _context = context;
            _restaurant = restaurantService;
            _turisticObject = turisticObjectService;
            _wrapper = wrapper;
            _recommenderRestaurants = recommenderR;
            _recommenderTO = recommenderT;
          
        }

        public IActionResult Index()
        {
            RecommendationsViewModel ItemsRecommended = new RecommendationsViewModel();
            List<Restaurant> restaurantsInCity;
            List<TuristicObject> closestTuristicObjects;
            List<TuristicObject> topTuristicObjects;
            topTuristicObjects = _turisticObject.Get().OrderBy(t => t.AverageRating).Take(5).ToList();

            var currentUser = this.ControllerContext.HttpContext.User.Identity.Name;
            if (_context.ApplicationUsers.FirstOrDefault(x => x.UserName == currentUser) != null)
            {
                int userId = _context.ApplicationUsers.FirstOrDefault(x => x.UserName == currentUser).Id;
                ItemsRecommended.UserId = userId;

                List<int> recommendationRestaurantsIds = _recommenderRestaurants.GetRestaurantsRecommendations(userId);
                List<int> recommendationTOIds=_recommenderTO.GetTuristicObjectsRecommendations(userId);

                ItemsRecommended.RestaurantRecommended = _recommenderRestaurants.GetRestaurantsRecommended(recommendationRestaurantsIds);
                ItemsRecommended.TuristicObjectsRecommended = _recommenderTO.GetTuristicObjectsRecommended(recommendationTOIds);

                var city = _context.ApplicationUsers.First(U => U.UserName == this.ControllerContext.HttpContext.User.Identity.Name).City;
                restaurantsInCity = _restaurant.Get().Where(R => R.City == city).OrderBy(r => r.AverageRating).Take(5).ToList();
                closestTuristicObjects = _turisticObject.Get().Where(R => R.City == city).Take(5).ToList();

                ItemsRecommended.RestaurantsInCity = restaurantsInCity;
                ItemsRecommended.ClosestTuristicObjects = closestTuristicObjects;
                ItemsRecommended.TopTuristicObjects = topTuristicObjects;


                return View(ItemsRecommended);
            }
            else { 
                var basicList = new RecommendationsViewModel()
                {
                RestaurantRecommended = null,
                RestaurantsInCity = null,
                ClosestTuristicObjects = null,
                TopTuristicObjects = topTuristicObjects
                };
                return View(basicList);
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
