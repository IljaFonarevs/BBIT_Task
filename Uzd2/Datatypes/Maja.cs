using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;


namespace Uzd2.Datatypes
{
    public class Maja
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

        public ICollection<Dzivoklis>?  Dzivoklis { get; set; }
         

    }
}
