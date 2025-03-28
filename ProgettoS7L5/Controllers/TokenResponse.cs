using System.ComponentModel.DataAnnotations;

namespace ProgettoS7L5.Controllers
{
    public class TokenResponse
    {

        [Required]
        public string Token { get; set; }
        public DateTime Expires { get; set; }
    }
}
