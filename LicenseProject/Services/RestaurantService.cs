using LicenseProject.Models;
using LicenseProject.Wrapper.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LicenseProject.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IProjectWrapper _wrapper;
        private readonly Context _context;
        public RestaurantService(IProjectWrapper wrapper, Context context)
        {
            _wrapper = wrapper;
            _context = context;

        }
        public IEnumerable<Restaurant> Restaurants => _context.Restaurants.Include(c => c.RestaurantsCategories).Include(c => c.Reviews);
        public List<Restaurant> Get()
        {
            return _wrapper.Restaurant.GetAll().Include(c => c.RestaurantsCategories).Include(c => c.Reviews).ToList();
        }
        public Restaurant GetRestaurantById(int? id)
        {
            return _wrapper.Restaurant.FindByCondition(r => r.RestaurantId.Equals(id))
           .FirstOrDefault();
        }

        public void Create(Restaurant restaurant)
        {
            _wrapper.Restaurant.Create(restaurant);
            _wrapper.Save();

        }
        public void Delete(int? id)
        {
            var restaurant = _wrapper.Restaurant.Get().FirstOrDefault(m => m.RestaurantId == id);
            _wrapper.Restaurant.Delete(restaurant);
            _wrapper.Save();

        }

        public void Update(Restaurant restaurant)
        {
            _wrapper.Restaurant.Update(restaurant);
            _wrapper.Save();
        }
    }
}
