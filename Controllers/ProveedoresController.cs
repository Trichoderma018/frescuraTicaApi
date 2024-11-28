using FrescuraApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FrescuraApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProveedoresController(FreshContext context) : ControllerBase
    {
        private readonly FreshContext _context = context;

        // GET: api/Proveedores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Proveedores>>> GetProveedores()
        {
            return await _context.Proveedores.ToListAsync();
        }

        // GET: api/Proveedores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Proveedores>> GetProveedor(int id)
        {
            var proveedor = await _context.Proveedores.FindAsync(id);

            if (proveedor == null)
            {
                return NotFound();
            }

            return proveedor;
        }

        // POST: api/Proveedores
        [HttpPost]
        public async Task<ActionResult<Proveedores>> PostProveedor(Proveedores proveedor)
        {
            // Validar que el NombreComercial no sea duplicado
            if (await _context.Proveedores.AnyAsync(p => p.NombreComercial == proveedor.NombreComercial))
            {
                return BadRequest("El Nombre Comercial ya existe.");
            }

            // Validar que TelefonoID exista si es proporcionado
            if (proveedor.TelefonoID != 0 && !await _context.Telefono.AnyAsync(t => t.TelefonoID == proveedor.TelefonoID))
            {
                return BadRequest("El TelefonoID especificado no existe.");
            }

            _context.Proveedores.Add(proveedor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProveedor", new { id = proveedor.ProveedoresID }, proveedor);
        }

        // PUT: api/Proveedores/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProveedor(int id, Proveedores proveedor)
        {
            if (id != proveedor.ProveedoresID)
            {
                return BadRequest("El ID del proveedor no coincide.");
            }

            // Validar que el NombreComercial no sea duplicado por otro proveedor
            if (await _context.Proveedores.AnyAsync(p => p.NombreComercial == proveedor.NombreComercial && p.ProveedoresID != id))
            {
                return BadRequest("El Nombre Comercial ya está en uso por otro proveedor.");
            }

            // Validar que TelefonoID exista si es proporcionado
            if (proveedor.TelefonoID != 0 && !await _context.Telefono.AnyAsync(t => t.TelefonoID == proveedor.TelefonoID))
            {
                return BadRequest("El TelefonoID especificado no existe.");
            }

            _context.Entry(proveedor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProveedorExists(id))
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

        // DELETE: api/Proveedores/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProveedor(int id)
        {
            var proveedor = await _context.Proveedores.FindAsync(id);
            if (proveedor == null)
            {
                return NotFound();
            }

            _context.Proveedores.Remove(proveedor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProveedorExists(int id)
        {
            return _context.Proveedores.Any(e => e.ProveedoresID == id);
        }
    }
}
