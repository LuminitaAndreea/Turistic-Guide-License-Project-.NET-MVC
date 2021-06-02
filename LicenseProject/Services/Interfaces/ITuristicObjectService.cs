using LicenseProject.Models;
using System.Collections.Generic;

namespace LicenseProject.Services
{
    public interface ITuristicObjectService
    {
        IEnumerable<TuristicObject> TuristicObjects { get; }

        void Create(TuristicObject turisticObject);
        void Delete(int id);
        List<TuristicObject> Get();
        TuristicObject GetTuristicObjectById(int? id);
        void Update(TuristicObject turisticObject);
    }
}