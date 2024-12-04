using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FrescuraApi.Models;

namespace FrescuraApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProveedoresController : ControllerBase
    {
        private readonly FreshContext _context;

        public ProveedoresController(FreshContext context)
        {
            _context = context;
        }

        // GET: api/Proveedores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Proveedores>>> GetProveedores()
        {
            return await _context.Set<Proveedores>()
                                 .Include(p => p.Telefonos)
                                 .Include(p => p.Pedidos)
                                 .ToListAsync();
        }

        // GET: api/Proveedores/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Proveedores>> GetProveedor(int id)
        {
            var proveedor = await _context.Set<Proveedores>()
                                          .Include(p => p.Telefonos)
                                          .Include(p => p.Pedidos)
                                          .FirstOrDefaultAsync(p => p.ProveedoreID == id);

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
            _context.Set<Proveedores>().Add(proveedor);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProveedor), new { id = proveedor.ProveedoreID }, proveedor);
        }

        // PUT: api/Proveedores/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProveedor(int id, Proveedores proveedor)
        {
            if (id != proveedor.ProveedoreID)
            {
                return BadRequest("El ID del proveedor no coincide.");
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

        // DELETE: api/Proveedores/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProveedor(int id)
        {
            var proveedor = await _context.Set<Proveedores>().FindAsync(id);
            if (proveedor == null)
            {
                return NotFound();
            }

            _context.Set<Proveedores>().Remove(proveedor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProveedorExists(int id)
        {
            return _context.Set<Proveedores>().Any(e => e.ProveedoreID == id);
        }
    }
}
