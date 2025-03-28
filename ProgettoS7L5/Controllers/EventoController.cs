using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProgettoS7L5.Data;
using ProgettoS7L5.DTO;
using ProgettoS7L5.Models;

[Route("api/[controller]")]
[ApiController]
public class EventoController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public EventoController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/eventi
    [HttpGet]
    public async Task<IActionResult> GetEventi()
    {
        var eventi = await _context.Eventi
            .Include(e => e.Artista)
            .Select(e => new EventoResponseDto
            {
                EventoId = e.EventoId,
                Titolo = e.Titolo,
                Data = e.Data,
                Luogo = e.Luogo,
                Artista = new ArtistaDto
                {
                    Nome = e.Artista.Nome,
                    Genere = e.Artista.Genere,
                    Biografia = e.Artista.Biografia
                }
            }).ToListAsync();

        return Ok(eventi);
    }

    // GET: api/eventi/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetEvento(int id)
    {
        var evento = await _context.Eventi
            .Include(e => e.Artista)
            .Where(e => e.EventoId == id)
            .Select(e => new EventoResponseDto
            {
                EventoId = e.EventoId,
                Titolo = e.Titolo,
                Data = e.Data,
                Luogo = e.Luogo,
                Artista = new ArtistaDto
                {
                    Nome = e.Artista.Nome,
                    Genere = e.Artista.Genere,
                    Biografia = e.Artista.Biografia
                }
            }).FirstOrDefaultAsync();

        if (evento == null)
        {
            return NotFound();
        }

        return Ok(evento);
    }

    // POST: api/eventi
    [HttpPost]
    [Authorize(Roles = "Amministratore")]
    public async Task<IActionResult> CreateEvento([FromBody] EventoDto eventoDto)
    {
        if (eventoDto == null)
        {
            return BadRequest();
        }

        var evento = new Evento
        {
            Titolo = eventoDto.Titolo,
            Data = eventoDto.Data,
            Luogo = eventoDto.Luogo,
            ArtistaId = eventoDto.ArtistaId
        };

        _context.Eventi.Add(evento);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetEvento), new { id = evento.EventoId }, evento);
    }

    // PUT: api/eventi/{id}
    [HttpPut("{id}")]
    [Authorize(Roles = "Amministratore")]
    public async Task<IActionResult> UpdateEvento(int id, [FromBody] EventoDto eventoDto)
    {
        if (id <= 0 || eventoDto == null)
        {
            return BadRequest();
        }

        var evento = await _context.Eventi.FindAsync(id);
        if (evento == null)
        {
            return NotFound();
        }

        evento.Titolo = eventoDto.Titolo;
        evento.Data = eventoDto.Data;
        evento.Luogo = eventoDto.Luogo;
        evento.ArtistaId = eventoDto.ArtistaId;

        _context.Entry(evento).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/eventi/{id}
    [HttpDelete("{id}")]
    [Authorize(Roles = "Amministratore")]
    public async Task<IActionResult> DeleteEvento(int id)
    {
        var evento = await _context.Eventi.FindAsync(id);
        if (evento == null)
        {
            return NotFound();
        }

        _context.Eventi.Remove(evento);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}

