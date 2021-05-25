using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LicenseProject.Models
{
    public class FavoriteTuristicObject
    {
        [Key] public int FavoriteTuristicObjectId { get; set; }
        public TuristicObject TuristicObject { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
