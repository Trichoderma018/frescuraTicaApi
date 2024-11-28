using FrescuraApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FrescuraApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagoController(FreshContext context) : ControllerBase
    {
        private readonly FreshContext _context = context;

        // GET: api/Pago
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pago>>> GetPagos()
        {
            return await _context.Pago.ToListAsync();
        }

        // GET: api/Pago/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pago>> GetPago(int id)
        {
            var pago = await _context.Pago.FindAsync(id);

            if (pago == null)
            {
                return NotFound();
            }

            return pago;
        }

        // POST: api/Pago
        [HttpPost]
        public async Task<ActionResult<Pago>> PostPago(Pago pago)
        {
            // Validaciones de las relaciones
            if (!await _context.Tarjeta.AnyAsync(t => t.TarjetaID == pago.TarjetaID))
            {
                return BadRequest("El TarjetaID especificado no existe.");
            }

            if (!await _context.Pedido.AnyAsync(p => p.PedidoID == pago.PedidoID))
            {
                return BadRequest("El PedidoID especificado no existe.");
            }

            if (!await _context.Usuarios.AnyAsync(u => u.UsuariosID == pago.UsuarioID))
            {
                return BadRequest("El UsuarioID especificado no existe.");
            }

            // Validar que el precio total coincida con el del pedido relacionado
            var pedido = await _context.Pedido.FindAsync(pago.PedidoID);
            if (pedido != null && pedido.PrecioTotal != pago.PrecioTotal)
            {
                return BadRequest("El PrecioTotal no coincide con el precio del Pedido.");
            }

            _context.Pago.Add(pago);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPago", new { id = pago.PagoID }, pago);
        }

        // PUT: api/Pago/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPago(int id, Pago pago)
        {
            if (id != pago.PagoID)
            {
                return BadRequest("El ID del pago no coincide.");
            }

            // Validaciones de las relaciones
            if (!await _context.Tarjeta.AnyAsync(t => t.TarjetaID == pago.TarjetaID))
            {
                return BadRequest("El TarjetaID especificado no existe.");
            }

            if (!await _context.Pedido.AnyAsync(p => p.PedidoID == pago.PedidoID))
            {
                return BadRequest("El PedidoID especificado no existe.");
            }

            if (!await _context.Usuarios.AnyAsync(u => u.UsuariosID == pago.UsuarioID))
            {
                return BadRequest("El UsuarioID especificado no existe.");
            }

            _context.Entry(pago).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PagoExists(id))
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

        // DELETE: api/Pago/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePago(int id)
        {
            var pago = await _context.Pago.FindAsync(id);
            if (pago == null)
            {
                return NotFound();
            }

            _context.Pago.Remove(pago);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PagoExists(int id)
        {
            return _context.Pago.Any(e => e.PagoID == id);
        }
    }
}
