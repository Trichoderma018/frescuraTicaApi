using FrescuraApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FrescuraApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TelefonoController(FreshContext context) : ControllerBase
    {
        private readonly FreshContext _context = context;

        // GET: api/Telefono
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Telefono>>> GetTelefonos()
        {
            return await _context.Telefono.ToListAsync();
        }

        // GET: api/Telefono/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Telefono>> GetTelefono(int id)
        {
            var telefono = await _context.Telefono.FindAsync(id);

            if (telefono == null)
            {
                return NotFound();
            }

            return telefono;
        }

        // POST: api/Telefono
        [HttpPost]
        public async Task<ActionResult<Telefono>> PostTelefono(Telefono telefono)
        {
            // Validar que al menos un número de teléfono sea proporcionado
            if (string.IsNullOrWhiteSpace(telefono.Telefono1))
            {
                return BadRequest("Debe proporcionar al menos un número de teléfono.");
            }

            // Validar existencia de UsuariosID y ProveedoresID (si son proporcionados)
            if (telefono.UsuariosID != 0 && !await _context.Usuarios.AnyAsync(u => u.UsuariosID == telefono.UsuariosID))
            {
                return BadRequest("El UsuariosID especificado no existe.");
            }

            if (telefono.ProveedoresID != 0 && !await _context.Proveedores.AnyAsync(p => p.ProveedoresID == telefono.ProveedoresID))
            {
                return BadRequest("El ProveedoresID especificado no existe.");
            }

            _context.Telefono.Add(telefono);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTelefono", new { id = telefono.TelefonoID }, telefono);
        }

        // PUT: api/Telefono/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTelefono(int id, Telefono telefono)
        {
            if (id != telefono.TelefonoID)
            {
                return BadRequest("El ID del teléfono no coincide.");
            }

            // Validar existencia de UsuariosID y ProveedoresID (si son proporcionados)
            if (telefono.UsuariosID != 0 && !await _context.Usuarios.AnyAsync(u => u.UsuariosID == telefono.UsuariosID))
            {
                return BadRequest("El UsuariosID especificado no existe.");
            }

            if (telefono.ProveedoresID != 0 && !await _context.Proveedores.AnyAsync(p => p.ProveedoresID == telefono.ProveedoresID))
            {
                return BadRequest("El ProveedoresID especificado no existe.");
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

        // DELETE: api/Telefono/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTelefono(int id)
        {
            var telefono = await _context.Telefono.FindAsync(id);
            if (telefono == null)
            {
                return NotFound();
            }

            _context.Telefono.Remove(telefono);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TelefonoExists(int id)
        {
            return _context.Telefono.Any(e => e.TelefonoID == id);
        }
    }
}
