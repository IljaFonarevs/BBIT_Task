using System.ComponentModel.DataAnnotations;
using Uzd2.Datatypes;

namespace Uzd2.DTOs
{
    public class MajaDTO
    {

        [Key]
        public long MajaNumurs { get; set; }
        [MaxLength(255)]
        public string? Iela { get; set; }
        [MaxLength(255)]
        public string? Pilseta { get; set; }
        [MaxLength(255)]
        public string? Valsts { get; set; }
        [MaxLength(255)]
        public string? PastIndeks { get; set; }
    }
}
