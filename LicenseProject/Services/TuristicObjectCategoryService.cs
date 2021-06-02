using LicenseProject.Models;
using LicenseProject.Wrapper.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LicenseProject.Services
{
    public class TuristicObjectCategoryService : ITuristicObjectCategoryService
    {
        private readonly IProjectWrapper _wrapper;
        private readonly Context _context;
        public TuristicObjectCategoryService(IProjectWrapper wrapper, Context context)
        {
            _wrapper = wrapper;
            _context = context;

        }
        public IEnumerable<TuristicObjectCategory> TuristicObjectCategories => _context.TuristicObjectCategories.Include(c => c.Category).Include(c => c.TuristicObject);
        public List<TuristicObjectCategory> Get()
        {
            return _wrapper.TuristicObjectCategory.GetAll().Include(c => c.Category).Include(c => c.TuristicObject).ToList();
        }
        public TuristicObjectCategory GetTuristicObjectCategoryById(int? id)
        {
            return _wrapper.TuristicObjectCategory.FindByCondition(r => r.TuristicObjectCId.Equals(id))
           .FirstOrDefault();
        }

        public void Create(TuristicObjectCategory turisticObjectCategory)
        {
            _wrapper.TuristicObjectCategory.Create(turisticObjectCategory);
            _wrapper.Save();

        }
        public void Delete(int? id)
        {
            var TuristicObjectCategory = _wrapper.TuristicObjectCategory.Get().FirstOrDefault(m => m.TuristicObjectCId == id);
            _wrapper.TuristicObjectCategory.Delete(TuristicObjectCategory);
            _wrapper.Save();

        }

        public void Update(TuristicObjectCategory TuristicObjectCategory)
        {

            _wrapper.TuristicObjectCategory.Update(TuristicObjectCategory);
            _wrapper.Save();
        }
    }
}
