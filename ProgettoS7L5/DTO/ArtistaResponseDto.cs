namespace ProgettoS7L5.DTO
{
    public class ArtistaResponseDto
    {
        public int ArtistaId { get; set; }
        public string Nome { get; set; }
        public string Genere { get; set; }
        public string Biografia { get; set; }
        public List<EventoDto> Eventi { get; set; }
    }

}
