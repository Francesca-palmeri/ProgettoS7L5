using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using ProgettoS7L5.Models;

namespace ProgettoS7L5.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Surname { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DeletionDate { get; set; }
        public ICollection<ApplicationUserRole> ApplicationRoles { get; set; }
        public ICollection<Biglietto> Biglietti { get; set; } 
    }
}
