using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FrescuraApi.Models;

namespace FrescuraApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarjetasController : ControllerBase
    {
        private readonly FreshContext _context;

        public TarjetasController(FreshContext context)
        {
            _context = context;
        }

        // GET: api/Tarjetas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tarjeta>>> GetTarjetas()
        {
            return await _context.Set<Tarjeta>()
                                 .Include(t => t.Usuarios)
                                 .Include(t => t.Pagos)
                                 .ToListAsync();
        }

        // GET: api/Tarjetas/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Tarjeta>> GetTarjeta(int id)
        {
            var tarjeta = await _context.Set<Tarjeta>()
                                        .Include(t => t.Usuarios)
                                        .Include(t => t.Pagos)
                                        .FirstOrDefaultAsync(t => t.TarjetaID == id);

            if (tarjeta == null)
            {
                return NotFound();
            }

            return tarjeta;
        }

        // POST: api/Tarjetas
        [HttpPost]
        public async Task<ActionResult<Tarjeta>> PostTarjeta(Tarjeta tarjeta)
        {
            try
            {
                _context.Set<Tarjeta>().Add(tarjeta);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetTarjeta), new { id = tarjeta.TarjetaID }, tarjeta);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // PUT: api/Tarjetas/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTarjeta(int id, Tarjeta tarjeta)
        {
            if (id != tarjeta.TarjetaID)
            {
                return BadRequest("El ID de la tarjeta no coincide.");
            }

            _context.Entry(tarjeta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TarjetaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }

            return NoContent();
        }

        // DELETE: api/Tarjetas/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTarjeta(int id)
        {
            try
            {
                var tarjeta = await _context.Set<Tarjeta>().FindAsync(id);
                if (tarjeta == null)
                {
                    return NotFound();
                }

                _context.Set<Tarjeta>().Remove(tarjeta);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        private bool TarjetaExists(int id)
        {
            return _context.Set<Tarjeta>().Any(e => e.TarjetaID == id);
        }
    }
}