using System.ComponentModel.DataAnnotations;

namespace ProgettoS7L5.DTO
{
    public class RegisterDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public string Nickname { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "La password deve avere almeno 6 caratteri.")]
        public string Password { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        public DateTime BirthDate { get; set; }


    }
}
