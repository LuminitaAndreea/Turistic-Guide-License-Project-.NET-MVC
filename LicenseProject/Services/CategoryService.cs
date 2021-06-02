using LicenseProject.Models;
using LicenseProject.Wrapper.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LicenseProject.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IProjectWrapper _wrapper;
        private readonly Context _context;
        public CategoryService(IProjectWrapper wrapper, Context context)
        {
            _wrapper = wrapper;
            _context = context;

        }
        public IEnumerable<Category> Categories => _context.Categories.Include(c => c.TuristicObjects).Include(c => c.Restaurants);
        public List<Category> Get()
        {
            return _wrapper.Category.GetAll().Include(c => c.TuristicObjects).Include(c => c.Restaurants).ToList();
        }
        public Category GetCategoryById(int? id)
        {
            return _wrapper.Category.FindByCondition(c => c.CategoryId.Equals(id))
           .FirstOrDefault();
        }

        public void Create(Category category)
        {
            _wrapper.Category.Create(category);
            _wrapper.Save();

        }
        public void Delete(int? id)
        {
            var category = _wrapper.Category.Get().FirstOrDefault(m => m.CategoryId == id);
            _wrapper.Category.Delete(category);
            _wrapper.Save();

        }

        public void Update(Category category)
        {

            _wrapper.Category.Update(category);
            _wrapper.Save();
        }
    }
}
