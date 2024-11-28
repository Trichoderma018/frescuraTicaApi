using FrescuraApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FrescuraApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarjetaController(FreshContext context) : ControllerBase
    {
        private readonly FreshContext _context = context;

        // GET: api/Tarjeta
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tarjeta>>> GetTarjetas()
        {
            return await _context.Tarjeta.ToListAsync();
        }

        // GET: api/Tarjeta/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tarjeta>> GetTarjeta(int id)
        {
            var tarjeta = await _context.Tarjeta.FindAsync(id);

            if (tarjeta == null)
            {
                return NotFound();
            }

            return tarjeta;
        }

        // POST: api/Tarjeta
        [HttpPost]
        public async Task<ActionResult<Tarjeta>> PostTarjeta(Tarjeta tarjeta)
        {
            // Validar que el UsuarioID exista
            if (!await _context.Usuarios.AnyAsync(u => u.UsuariosID == tarjeta.UsuarioID))
            {
                return BadRequest("El UsuarioID especificado no existe.");
            }

            // Validar longitud de Número de Tarjeta y CVV
            if (tarjeta.NumeroTarjeta.ToString().Length != 16)
            {
                return BadRequest("El Número de Tarjeta debe tener 16 dígitos.");
            }

            if (tarjeta.Cvv.ToString().Length != 3)
            {
                return BadRequest("El CVV debe tener 3 dígitos.");
            }

            // Validar que la fecha de expiración no sea pasada
            if (tarjeta.Expiracion < DateOnly.FromDateTime(DateTime.UtcNow))
            {
                return BadRequest("La fecha de expiración no puede ser pasada.");
            }

            _context.Tarjeta.Add(tarjeta);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTarjeta", new { id = tarjeta.TarjetaID }, tarjeta);
        }

        // PUT: api/Tarjeta/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTarjeta(int id, Tarjeta tarjeta)
        {
            if (id != tarjeta.TarjetaID)
            {
                return BadRequest("El ID de la tarjeta no coincide.");
            }

            // Validar que el UsuarioID exista
            if (!await _context.Usuarios.AnyAsync(u => u.UsuariosID == tarjeta.UsuarioID))
            {
                return BadRequest("El UsuarioID especificado no existe.");
            }

            // Validar longitud de Número de Tarjeta y CVV
            if (tarjeta.NumeroTarjeta.ToString().Length != 16)
            {
                return BadRequest("El Número de Tarjeta debe tener 16 dígitos.");
            }

            if (tarjeta.Cvv.ToString().Length != 3)
            {
                return BadRequest("El CVV debe tener 3 dígitos.");
            }

            // Validar que la fecha de expiración no sea pasada
            if (tarjeta.Expiracion < DateOnly.FromDateTime(DateTime.UtcNow))
            {
                return BadRequest("La fecha de expiración no puede ser pasada.");
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

            return NoContent();
        }

        // DELETE: api/Tarjeta/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTarjeta(int id)
        {
            var tarjeta = await _context.Tarjeta.FindAsync(id);
            if (tarjeta == null)
            {
                return NotFound();
            }

            _context.Tarjeta.Remove(tarjeta);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TarjetaExists(int id)
        {
            return _context.Tarjeta.Any(e => e.TarjetaID == id);
        }
    }
}
