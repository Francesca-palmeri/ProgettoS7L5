using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProgettoS7L5.Data;
using ProgettoS7L5.DTO;
using ProgettoS7L5.Models;
using System.Security.Claims;

[Route("api/[controller]")]
[ApiController]
public class BigliettoController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public BigliettoController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    // GET: api/biglietti
    [HttpGet]
    [Authorize(Roles = "Amministratore")]
    public async Task<IActionResult> GetBiglietti()
    {
        var biglietti = await _context.Biglietti
            .Include(b => b.Evento)
            .Include(b => b.ApplicationUser)
            .Select(b => new BigliettoResponseDto
            {
                BigliettoId = b.BigliettoId,
                Evento = new EventoDto
                {
                    Titolo = b.Evento.Titolo,
                    Data = b.Evento.Data,
                    Luogo = b.Evento.Luogo
                },
                UserEmail = b.ApplicationUser.Email,
                DataAcquisto = b.DataAcquisto
            }).ToListAsync();

        return Ok(biglietti);
    }

    // POST: api/biglietti
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> AcquistaBiglietto([FromBody] AcquistoBigliettoDto dto)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var user = await _userManager.FindByIdAsync(userId);

        var evento = await _context.Eventi.FindAsync(dto.EventoId);
        if (evento == null)
        {
            return NotFound("Evento non trovato");
        }

        var biglietto = new Biglietto
        {
            EventoId = dto.EventoId,
            UserId = userId,
            DataAcquisto = DateTime.Now
        };

        _context.Biglietti.Add(biglietto);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetBiglietto", new { id = biglietto.BigliettoId }, biglietto);
    }

    // DELETE: api/biglietti/{id}
    [HttpDelete("{id}")]
    [Authorize(Roles = "Amministratore")]
    public async Task<IActionResult> DeleteBiglietto(int id)
    {
        var biglietto = await _context.Biglietti.FindAsync(id);
        if (biglietto == null)
        {
            return NotFound();
        }

        _context.Biglietti.Remove(biglietto);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}

