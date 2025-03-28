using System.ComponentModel.DataAnnotations;

namespace ProgettoS7L5.Models
{
    public class Artista
    {
        [Key] 
        public int ArtistaId { get; set; }

        [Required(ErrorMessage = "Il nome dell'artista è obbligatorio.")]
        [StringLength(100, ErrorMessage = "Il nome dell'artista non può superare i 100 caratteri.")]
        public string Nome { get; set; }

        [StringLength(50, ErrorMessage = "Il genere non può superare i 50 caratteri.")]
        public string Genere { get; set; }

        [StringLength(500, ErrorMessage = "La biografia non può superare i 500 caratteri.")]
        public string Biografia { get; set; }

        // Relazione con gli eventi
        public ICollection<Evento> Eventi { get; set; }
    }
}
