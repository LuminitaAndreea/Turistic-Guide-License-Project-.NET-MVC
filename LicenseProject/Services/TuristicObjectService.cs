using LicenseProject.Models;
using LicenseProject.Wrapper.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LicenseProject.Services
{
    public class TuristicObjectService : ITuristicObjectService
    {
        private readonly IProjectWrapper _wrapper;
        private readonly Context _context;
        public TuristicObjectService(IProjectWrapper wrapper, Context context)
        {
            _wrapper = wrapper;
            _context = context;

        }
        public IEnumerable<TuristicObject> TuristicObjects => _context.TuristicObjects.Include(c => c.TuristicObjectsCategories).Include(c => c.Reviews);
        public List<TuristicObject> Get()
        {
            return _wrapper.TuristicObject.GetAll().Include(c => c.TuristicObjectsCategories).Include(c => c.Reviews).ToList();
        }
        public TuristicObject GetTuristicObjectById(int? id)
        {
            return _wrapper.TuristicObject.FindByCondition(t => t.TuristicObjectId.Equals(id))
           .FirstOrDefault();
        }

        public void Create(TuristicObject turisticObject)
        {
            _wrapper.TuristicObject.Create(turisticObject);
            _wrapper.Save();

        }
        public void Delete(int id)
        {
            var turisticObject = _wrapper.TuristicObject.Get().FirstOrDefault(m => m.TuristicObjectId == id);
            _wrapper.TuristicObject.Delete(turisticObject);
            _wrapper.Save();

        }

        public void Update(TuristicObject turisticObject)
        {

            _wrapper.TuristicObject.Update(turisticObject);
            _wrapper.Save();
        }
    }
}