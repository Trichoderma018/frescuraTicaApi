using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FrescuraApi.Models;

namespace FrescuraApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventariosController : ControllerBase
    {
        private readonly FreshContext _context;

        public InventariosController(FreshContext context)
        {
            _context = context;
        }

        // GET: api/Inventarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Inventario>>> GetInventarios()
        {
            return await _context.Set<Inventario>()
                                 .Include(i => i.Producto)
                                 .Include(i => i.Pedido)
                                 .ToListAsync();
        }

        // GET: api/Inventarios/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Inventario>> GetInventario(int id)
        {
            var inventario = await _context.Set<Inventario>()
                                           .Include(i => i.Producto)
                                           .Include(i => i.Pedido)
                                           .FirstOrDefaultAsync(i => i.InventarioID == id);

            if (inventario == null)
            {
                return NotFound();
            }

            return inventario;
        }

        // POST: api/Inventarios
        [HttpPost]
        public async Task<ActionResult<Inventario>> PostInventario(Inventario inventario)
        {
            try
            {
                // Validaciones
                var producto = await _context.Set<Producto>().FindAsync(inventario.ProductoID);
                if (producto == null)
                {
                    return BadRequest("El producto especificado no existe.");
                }

                var pedido = await _context.Set<Pedido>().FindAsync(inventario.PedidoID);
                if (pedido == null)
                {
                    return BadRequest("El pedido especificado no existe.");
                }

                // Cálculos automáticos
                inventario.TotalCompra = pedido.Cantidad * inventario.PrecioCompra;
                inventario.TotalVenta = pedido.Cantidad * inventario.PrecioVenta;
                inventario.Utilidad = inventario.TotalVenta - inventario.TotalCompra;
                inventario.FechaYHora = DateTime.UtcNow;

                _context.Set<Inventario>().Add(inventario);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetInventario), new { id = inventario.InventarioID }, inventario);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // PUT: api/Inventarios/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInventario(int id, Inventario inventario)
        {
            if (id != inventario.InventarioID)
            {
                return BadRequest("El ID del inventario no coincide.");
            }

            try
            {
                var producto = await _context.Set<Producto>().FindAsync(inventario.ProductoID);
                if (producto == null)
                {
                    return BadRequest("El producto especificado no existe.");
                }

                var pedido = await _context.Set<Pedido>().FindAsync(inventario.PedidoID);
                if (pedido == null)
                {
                    return BadRequest("El pedido especificado no existe.");
                }

                // Cálculos automáticos
                inventario.TotalCompra = pedido.Cantidad * inventario.PrecioCompra;
                inventario.TotalVenta = pedido.Cantidad * inventario.PrecioVenta;
                inventario.Utilidad = inventario.TotalVenta - inventario.TotalCompra;

                _context.Entry(inventario).State = EntityState.Modified;
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
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }

            return NoContent();
        }

        // DELETE: api/Inventarios/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInventario(int id)
        {
            try
            {
                var inventario = await _context.Set<Inventario>().FindAsync(id);
                if (inventario == null)
                {
                    return NotFound();
                }

                _context.Set<Inventario>().Remove(inventario);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        private bool InventarioExists(int id)
        {
            return _context.Set<Inventario>().Any(e => e.InventarioID == id);
        }
    }
}