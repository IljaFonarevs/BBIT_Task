using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Uzd2.Datatypes
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        string login { get; set; }

        [Key]
        string password { get; set; }
        
        [Required]
        public int apartNumber { get; set; }
        
    }
}
