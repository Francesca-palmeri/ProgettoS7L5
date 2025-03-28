namespace ProgettoS7L5.DTO
{
    public class BigliettoDto
    {
        public int BigliettoId { get; set; }
        public string EventoTitolo { get; set; }
        public DateTime DataAcquisto { get; set; }
        public string UserEmail { get; set; }  // Per riferirsi all'utente che ha acquistato il biglietto
    }

}
