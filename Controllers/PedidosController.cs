using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FrescuraApi.Models;

namespace FrescuraApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidosController : ControllerBase
    {
        private readonly FreshContext _context;

        public PedidosController(FreshContext context)
        {
            _context = context;
        }

        // GET: api/Pedidos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pedido>>> GetPedidos()
        {
            return await _context.Set<Pedido>()
                                 .Include(p => p.Producto)
                                 .Include(p => p.Usuario)
                                 .Include(p => p.Proveedor)
                                 .Include(p => p.Inventarios)
                                 .Include(p => p.Pagos)
                                 .ToListAsync();
        }

        // GET: api/Pedidos/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Pedido>> GetPedido(int id)
        {
            var pedido = await _context.Set<Pedido>()
                                       .Include(p => p.Producto)
                                       .Include(p => p.Usuario)
                                       .Include(p => p.Proveedor)
                                       .Include(p => p.Inventarios)
                                       .Include(p => p.Pagos)
                                       .FirstOrDefaultAsync(p => p.PedidoID == id);

            if (pedido == null)
            {
                return NotFound();
            }

            return pedido;
        }

        // POST: api/Pedidos
        [HttpPost]
        public async Task<ActionResult<Pedido>> PostPedido(Pedido pedido)
        {
            try
            {
                // Validación: PrecioTotal = Cantidad * Producto.Precio
                var producto = await _context.Set<Producto>().FindAsync(pedido.ProductoID);
                if (producto == null)
                {
                    return BadRequest("El producto especificado no existe.");
                }
                pedido.PrecioTotal = (int)(pedido.Cantidad * producto.Precio);

                _context.Set<Pedido>().Add(pedido);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetPedido), new { id = pedido.PedidoID }, pedido);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // PUT: api/Pedidos/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPedido(int id, Pedido pedido)
        {
            if (id != pedido.PedidoID)
            {
                return BadRequest("El ID del pedido no coincide.");
            }

            try
            {
                var producto = await _context.Set<Producto>().FindAsync(pedido.ProductoID);
                if (producto == null)
                {
                    return BadRequest("El producto especificado no existe.");
                }
                pedido.PrecioTotal = (int)(pedido.Cantidad * producto.Precio);

                _context.Entry(pedido).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PedidoExists(id))
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

        // DELETE: api/Pedidos/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePedido(int id)
        {
            try
            {
                var pedido = await _context.Set<Pedido>().FindAsync(id);
                if (pedido == null)
                {
                    return NotFound();
                }

                _context.Set<Pedido>().Remove(pedido);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        private bool PedidoExists(int id)
        {
            return _context.Set<Pedido>().Any(e => e.PedidoID == id);
        }
    }
}