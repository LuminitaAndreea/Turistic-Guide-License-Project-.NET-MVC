using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LicenseProject.Models
{
    public class TuristicObjectCategory
    {
        [Key] public int TuristicObjectCId { get; set; }
        public int CategoryId { get; set; }
        public int TuristicObjectId { get; set; }

        public TuristicObject TuristicObject { get; set; }
        public Category Category { get; set; }
    }
}
