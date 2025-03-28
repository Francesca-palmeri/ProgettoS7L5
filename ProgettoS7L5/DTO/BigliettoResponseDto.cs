namespace ProgettoS7L5.DTO
{
    public class BigliettoResponseDto
    {
        public int BigliettoId { get; set; }
        public EventoDto Evento { get; set; }
        public string UserEmail { get; set; }
        public DateTime DataAcquisto { get; set; }
    }

}
