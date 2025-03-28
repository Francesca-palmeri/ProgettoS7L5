namespace ProgettoS7L5.DTO
{
    public class EventoDto
    {
        public string Titolo { get; set; }
        public DateTime Data { get; set; }
        public string Luogo { get; set; }
        public int ArtistaId { get; set; }  // Riferimento all'Artista
    }

}
