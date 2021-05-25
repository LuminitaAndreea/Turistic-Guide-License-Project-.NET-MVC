using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LicenseProject.Models
{
    public class FavoriteTuristicObject
    {
        public int FavoriteTuristicObjectId { get; set; }
        public TuristicObject TuristicObject { get; set; }
    }
}
