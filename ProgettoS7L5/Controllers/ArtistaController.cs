using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProgettoS7L5.Data;
using ProgettoS7L5.DTO;
using ProgettoS7L5.Models;

[Route("api/[controller]")]
[ApiController]
public class ArtistaController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ArtistaController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/artisti
    [HttpGet]
    public async Task<IActionResult> GetArtisti()
    {
        var artisti = await _context.Artisti
            .Select(a => new ArtistaDto
            {
                Nome = a.Nome,
                Genere = a.Genere,
                Biografia = a.Biografia
            }).ToListAsync();

        return Ok(artisti);
    }

    // GET: api/artisti/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetArtista(int id)
    {
        var artista = await _context.Artisti
            .Where(a => a.ArtistaId == id)
            .Select(a => new ArtistaDto
            {
                Nome = a.Nome,
                Genere = a.Genere,
                Biografia = a.Biografia
            }).FirstOrDefaultAsync();

        if (artista == null)
        {
            return NotFound();
        }

        return Ok(new ArtistaDto
        {
            Nome = artista.Nome,
            Genere = artista.Genere,
            Biografia = artista.Biografia
        });
    }

    // POST: api/artisti
    [HttpPost]
    [Authorize(Roles = "Amministratore")]
    public async Task<IActionResult> CreateArtista([FromBody] ArtistaDto artistaDto)
    {
        if (artistaDto == null)
        {
            return BadRequest();
        }

        var artista = new Artista
        {
            Nome = artistaDto.Nome,
            Genere = artistaDto.Genere,
            Biografia = artistaDto.Biografia
        };

        _context.Artisti.Add(artista);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetArtista), new { id = artista.ArtistaId }, artista);
    }

    // PUT: api/artisti/{id}
    [HttpPut("{id}")]
    [Authorize(Roles = "Amministratore")]
    public async Task<IActionResult> UpdateArtista(int id, [FromBody] ArtistaDto artistaDto)
    {
        if (id <= 0 || artistaDto == null)
        {
            return BadRequest();
        }

        var artista = await _context.Artisti.FindAsync(id);
        if (artista == null)
        {
            return NotFound();
        }

        artista.Nome = artistaDto.Nome;
        artista.Genere = artistaDto.Genere;
        artista.Biografia = artistaDto.Biografia;

        _context.Entry(artista).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/artisti/{id}
    [HttpDelete("{id}")]
    [Authorize(Roles = "Amministratore")]
    public async Task<IActionResult> DeleteArtista(int id)
    {
        var artista = await _context.Artisti.FindAsync(id);
        if (artista == null)
        {
            return NotFound();
        }

        _context.Artisti.Remove(artista);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
