using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProgettoS7L5.Models
{
    public class Evento
    {
        [Key] 
        public int EventoId { get; set; }

        [Required(ErrorMessage = "Il titolo dell'evento è obbligatorio.")]
        [StringLength(150, ErrorMessage = "Il titolo dell'evento non può superare i 150 caratteri.")]
        public string Titolo { get; set; }

        [Required(ErrorMessage = "La data dell'evento è obbligatoria.")]
        public DateTime Data { get; set; }

        [Required(ErrorMessage = "Il luogo dell'evento è obbligatorio.")]
        [StringLength(200, ErrorMessage = "Il luogo non può superare i 200 caratteri.")]
        public string Luogo { get; set; }

        // Relazione con Artista (Molti a uno)
        [Required(ErrorMessage = "L'ID dell'artista è obbligatorio.")]
        [ForeignKey("Artista")]
        public int ArtistaId { get; set; }
        public Artista Artista { get; set; }

        // Relazione con Biglietto (Uno a molti)
        public ICollection<Biglietto> Biglietti { get; set; }
    }
}
