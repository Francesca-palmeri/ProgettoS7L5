using Microsoft.AspNetCore.Identity;
using ProgettoS7L5.Models;

namespace ProgettoS7L5.Models
{
    public class ApplicationUserRole : IdentityUserRole<string>
    {

        public ApplicationUser User { get; set; }
        public ApplicationRole Role { get; set; }
    }
}
