using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LicenseProject.Models
{
    public class Review
    {
        [Key] public int ReviewId { get; set; }
        public string ReviewDescription { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public int Rate { get; set; }
        public virtual TuristicObject TuristicObject { get; set; }
        public virtual Restaurant Restaurant { get; set; }
        public DateTime Date { get; set; }
    }
}
