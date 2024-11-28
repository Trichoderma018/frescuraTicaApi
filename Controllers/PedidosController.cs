using FrescuraApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FrescuraApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController(FreshContext context) : ControllerBase
    {
        private readonly FreshContext _context = context;

        // GET: api/Pedido
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pedido>>> GetPedidos()
        {
            return await _context.Pedido.ToListAsync();
        }

        // GET: api/Pedido/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pedido>> GetPedido(int id)
        {
            var pedido = await _context.Pedido.FindAsync(id);

            if (pedido == null)
            {
                return NotFound();
            }

            return pedido;
        }

        // POST: api/Pedido
        [HttpPost]
        public async Task<ActionResult<Pedido>> PostPedido(Pedido pedido)
        {
            // Validación básica de las relaciones
            if (!await _context.Producto.AnyAsync(p => p.ProductoID == pedido.ProductoID))
            {
                return BadRequest("El ProductoID especificado no existe.");
            }

            if (!await _context.Proveedores.AnyAsync(p => p.ProveedoresID == pedido.ProveedoresID))
            {
                return BadRequest("El ProveedoresID especificado no existe.");
            }

            if (!await _context.Usuarios.AnyAsync(u => u.UsuariosID == pedido.UsuarioID))
            {
                return BadRequest("El UsuarioID especificado no existe.");
            }

            _context.Pedido.Add(pedido);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPedido", new { id = pedido.PedidoID }, pedido);
        }

        // PUT: api/Pedido/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPedido(int id, Pedido pedido)
        {
            if (id != pedido.PedidoID)
            {
                return BadRequest("El ID del pedido no coincide.");
            }

            // Validación básica de las relaciones
            if (!await _context.Producto.AnyAsync(p => p.ProductoID == pedido.ProductoID))
            {
                return BadRequest("El ProductoID especificado no existe.");
            }

            if (!await _context.Proveedores.AnyAsync(p => p.ProveedoresID == pedido.ProveedoresID))
            {
                return BadRequest("El ProveedoresID especificado no existe.");
            }

            if (!await _context.Usuarios.AnyAsync(u => u.UsuariosID == pedido.UsuarioID))
            {
                return BadRequest("El UsuarioID especificado no existe.");
            }

            _context.Entry(pedido).State = EntityState.Modified;

            try
            {
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

            return NoContent();
        }

        // DELETE: api/Pedido/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePedido(int id)
        {
            var pedido = await _context.Pedido.FindAsync(id);
            if (pedido == null)
            {
                return NotFound();
            }

            _context.Pedido.Remove(pedido);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PedidoExists(int id)
        {
            return _context.Pedido.Any(e => e.PedidoID == id);
        }
    }
}
