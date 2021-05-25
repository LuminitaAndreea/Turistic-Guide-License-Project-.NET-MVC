using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LicenseProject.Models
{
    public class FavoriteListTuristicObject
    {
        public string FavoriteListTuristicObjectId { get; set; }
        public List<FavoriteTuristicObject> FavoriteObjects { get; set; }
    }
}
