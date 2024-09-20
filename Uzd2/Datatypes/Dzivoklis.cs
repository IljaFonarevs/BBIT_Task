using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;


namespace Uzd2.Datatypes
{
    
    public class Dzivoklis
    {
        [Key]
        public long DzivNumurs { get; set; }
        [Range(1, 200)]
        public int? Stavs { get; set; }
        [Range(1, 1000)]
        public int? IstabSkaits { get; set; }
        public int? IeiedzSkaits { get; set; }
        [Precision(18, 2)]
        public double? Platiba { get; set; }
        [Precision(18, 2)]
        public double? DzivPlatiba { get; set; }

        [Required]
        public long MajaID { get; set; }

        public Maja? Maja { get; set; }

        public ICollection<DzivoklisIedzivotajs>? DzivIedz { get; set; }

        
    }
}
