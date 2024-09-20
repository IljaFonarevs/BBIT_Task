using System.ComponentModel.DataAnnotations;

namespace Uzd2.Datatypes
{
    public class Iedzivotajs
    {
        [Required]
        [MaxLength(255)]
        public string? Vards { get; set; }
        [Required]
        [MaxLength(255)]
        public string? Uzvards { get; set; }

        [Key]
        public long PersKods { get; set; }

        [Required]
        public long DzivNumurs { get; set; }

        public bool IsOwner { get; set; }

        public DateOnly? DzimDat { get; set; }

        public Dzivoklis? Dzivoklis { get; set; }

        public ICollection<DzivoklisIedzivotajs>? DzivIedz {  get; set; }

    }
}
