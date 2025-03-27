using Microsoft.AspNetCore.Identity;

namespace ProgettoS7L5.Models
{
    public class ApplicationRole : IdentityRole<string>
    {
        public ICollection<ApplicationUserRole> ApplicationUsers { get; set; }
    }
}
