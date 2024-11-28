using FrescuraApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FrescuraApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventarioController(FreshContext context) : ControllerBase
    {
        private readonly FreshContext _context = context;


        // GET: api/Inventario
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Inventario>>> GetInventarios()
        {
            return await _context.Inventario.ToListAsync();
        }

        // GET: api/Inventario/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Inventario>> GetInventario(int id)
        {
            var inventario = await _context.Inventario.FindAsync(id);

            if (inventario == null)
            {
                return NotFound();
            }

            return inventario;
        }

        // POST: api/Inventario
        [HttpPost]
        public async Task<ActionResult<Inventario>> PostInventario(Inventario inventario)
        {
            // Validaciones de las relaciones
            if (!await _context.Producto.AnyAsync(p => p.ProductoID == inventario.ProductoID))
            {
                return BadRequest("El ProductoID especificado no existe.");
            }

            if (!await _context.Pedido.AnyAsync(p => p.PedidoID == inventario.PedidoID))
            {
                return BadRequest("El PedidoID especificado no existe.");
            }

            // Calcular los campos derivados
            inventario.TotalCompra = inventario.PrecioCompra * inventario.SaveStock;
            inventario.TotalVenta = inventario.PrecioVenta * inventario.SaveStock;
            inventario.Utilidad = inventario.TotalVenta - inventario.TotalCompra;
            inventario.FechaYHora = DateTime.UtcNow;

            _context.Inventario.Add(inventario);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInventario", new { id = inventario.InventarioID }, inventario);
        }

        // PUT: api/Inventario/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInventario(int id, Inventario inventario)
        {
            if (id != inventario.InventarioID)
            {
                return BadRequest("El ID del inventario no coincide.");
            }

            // Validaciones de las relaciones
            if (!await _context.Producto.AnyAsync(p => p.ProductoID == inventario.ProductoID))
            {
                return BadRequest("El ProductoID especificado no existe.");
            }

            if (!await _context.Pedido.AnyAsync(p => p.PedidoID == inventario.PedidoID))
            {
                return BadRequest("El PedidoID especificado no existe.");
            }

            // Calcular los campos derivados
            inventario.TotalCompra = inventario.PrecioCompra * inventario.SaveStock;
            inventario.TotalVenta = inventario.PrecioVenta * inventario.SaveStock;
            inventario.Utilidad = inventario.TotalVenta - inventario.TotalCompra;
            inventario.FechaYHora = DateTime.UtcNow;

            _context.Entry(inventario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InventarioExists(id))
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

        // DELETE: api/Inventario/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInventario(int id)
        {
            var inventario = await _context.Inventario.FindAsync(id);
            if (inventario == null)
            {
                return NotFound();
            }

            _context.Inventario.Remove(inventario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InventarioExists(int id)
        {
            return _context.Inventario.Any(e => e.InventarioID == id);
        }
    }
}
