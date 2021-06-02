using LicenseProject.Models;
using LicenseProject.Wrapper.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LicenseProject.Services
{
    public class RestaurantCategoryService : IRestaurantCategoryService
    {
        private readonly IProjectWrapper _wrapper;
        private readonly Context _context;
        public RestaurantCategoryService(IProjectWrapper wrapper, Context context)
        {
            _wrapper = wrapper;
            _context = context;

        }
        public IEnumerable<RestaurantCategory> RestaurantCategories => _context.RestaurantsCategories.Include(c => c.Category).Include(c => c.Restaurant);
        public List<RestaurantCategory> Get()
        {
            return _wrapper.RestaurantCategory.GetAll().Include(c => c.Category).Include(c => c.Restaurant).ToList();
        }
        public RestaurantCategory GetRestaurantCategoryById(int? id)
        {
            return _wrapper.RestaurantCategory.FindByCondition(r => r.RestaurantCId.Equals(id))
           .FirstOrDefault();
        }

        public void Create(RestaurantCategory RestaurantCategory)
        {
            _wrapper.RestaurantCategory.Create(RestaurantCategory);
            _wrapper.Save();

        }
        public void Delete(int? id)
        {
            var RestaurantCategory = _wrapper.RestaurantCategory.Get().FirstOrDefault(m => m.RestaurantCId == id);
            _wrapper.RestaurantCategory.Delete(RestaurantCategory);
            _wrapper.Save();

        }

        public void Update(RestaurantCategory RestaurantCategory)
        {

            _wrapper.RestaurantCategory.Update(RestaurantCategory);
            _wrapper.Save();
        }
    }
}
