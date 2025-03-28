namespace ProgettoS7L5.DTO
{
    public class EventoResponseDto
    {
        public int EventoId { get; set; }
        public string Titolo { get; set; }
        public DateTime Data { get; set; }
        public string Luogo { get; set; }
        public ArtistaDto Artista { get; set; }
        public List<BigliettoDto> Biglietti { get; set; }
    }

}
