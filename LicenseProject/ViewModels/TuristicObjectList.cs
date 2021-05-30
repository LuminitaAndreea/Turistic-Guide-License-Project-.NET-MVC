using LicenseProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LicenseProject.ViewModels
{
    public class TuristicObjectList
    {
        public List<TuristicObject> TuristicObjects { get; set; }
        public List<Category> Categories { get; set; }
        public string CurrentCategory { get; set; }
    }
}
