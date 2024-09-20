using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Uzd2.Datatypes;

namespace Uzd2.DTOs
{
    public class DzivDTO
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

    }
}
