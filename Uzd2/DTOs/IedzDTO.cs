using System.ComponentModel.DataAnnotations;

namespace Uzd2.DTOs
{
    public class IedzDTO
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
    }
}
