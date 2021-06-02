using LicenseProject.Models;
using System.Collections.Generic;

namespace LicenseProject.Services
{
    public interface ITuristicObjectCategoryService
    {
        IEnumerable<TuristicObjectCategory> TuristicObjectCategories { get; }

        void Create(TuristicObjectCategory turisticObjectCategory);
        void Delete(int? id);
        List<TuristicObjectCategory> Get();
        TuristicObjectCategory GetTuristicObjectCategoryById(int? id);
        void Update(TuristicObjectCategory TuristicObjectCategory);
    }
}