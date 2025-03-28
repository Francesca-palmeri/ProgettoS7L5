using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProgettoS7L5.Models
{
    public class Biglietto
    {
        [Key]  
        public int BigliettoId { get; set; }

        [Required(ErrorMessage = "L'ID dell'evento è obbligatorio.")]
        [ForeignKey("Evento")]
        public int EventoId { get; set; }
        public Evento Evento { get; set; }

        [Required(ErrorMessage = "L'ID dell'utente è obbligatorio.")]
        public string UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        [Required(ErrorMessage = "La data dell'acquisto è obbligatoria.")]
        public DateTime DataAcquisto { get; set; }
    }
}
