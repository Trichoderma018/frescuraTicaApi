using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FrescuraApi.Models;

namespace FrescuraApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TelefonosController : ControllerBase
    {
        private readonly FreshContext _context;

        public TelefonosController(FreshContext context)
        {
            _context = context;
        }

        // GET: api/Telefonos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Telefono>>> GetTelefonos()
        {
            return await _context.Set<Telefono>()
                                 .Include(t => t.Usuario)
                                 .Include(t => t.Proveedor)
                                 .ToListAsync();
        }

        // GET: api/Telefonos/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Telefono>> GetTelefono(int id)
        {
            var telefono = await _context.Set<Telefono>()
                                         .Include(t => t.Usuario)
                                         .Include(t => t.Proveedor)
                                         .FirstOrDefaultAsync(t => t.TelefonoID == id);

            if (telefono == null)
            {
                return NotFound();
            }

            return telefono;
        }

        // POST: api/Telefonos
        [HttpPost]
        public async Task<ActionResult<Telefono>> PostTelefono(Telefono telefono)
        {
            _context.Set<Telefono>().Add(telefono);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTelefono), new { id = telefono.TelefonoID }, telefono);
        }

        // PUT: api/Telefonos/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTelefono(int id, Telefono telefono)
        {
            if (id != telefono.TelefonoID)
            {
                return BadRequest("El ID del teléfono no coincide.");
            }

            _context.Entry(telefono).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TelefonoExists(id))
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

        // DELETE: api/Telefonos/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTelefono(int id)
        {
            var telefono = await _context.Set<Telefono>().FindAsync(id);
            if (telefono == null)
            {
                return NotFound();
            }

            _context.Set<Telefono>().Remove(telefono);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TelefonoExists(int id)
        {
            return _context.Set<Telefono>().Any(e => e.TelefonoID == id);
        }
    }
}
