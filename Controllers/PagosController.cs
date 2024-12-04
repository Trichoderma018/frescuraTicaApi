using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FrescuraApi.Models;

namespace FrescuraApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagosController : ControllerBase
    {
        private readonly FreshContext _context;

        public PagosController(FreshContext context)
        {
            _context = context;
        }

        // GET: api/Pagos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pago>>> GetPagos()
        {
            return await _context.Set<Pago>()
                                 .Include(p => p.Tarjeta)
                                 .Include(p => p.Pedido)
                                 .Include(p => p.Usuarios)
                                 .ToListAsync();
        }

        // GET: api/Pagos/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Pago>> GetPago(int id)
        {
            var pago = await _context.Set<Pago>()
                                     .Include(p => p.Tarjeta)
                                     .Include(p => p.Pedido)
                                     .Include(p => p.Usuarios)
                                     .FirstOrDefaultAsync(p => p.PagoID == id);

            if (pago == null)
            {
                return NotFound();
            }

            return pago;
        }

        // POST: api/Pagos
        [HttpPost]
        public async Task<ActionResult<Pago>> PostPago(Pago pago)
        {
            try
            {
                // Validación: Verifica que el Pedido y Tarjeta existen
                var pedido = await _context.Set<Pedido>().FindAsync(pago.PedidoID);
                if (pedido == null)
                {
                    return BadRequest("El pedido especificado no existe.");
                }

                var tarjeta = await _context.Set<Tarjeta>().FindAsync(pago.TarjetaID);
                if (tarjeta == null)
                {
                    return BadRequest("La tarjeta especificada no existe.");
                }

                // Asigna el PrecioTotal desde el Pedido
                pago.PrecioTotal = pedido.PrecioTotal;

                // Validación del pago
                if (tarjeta.Pago)
                {
                    pago.Pagoo = true;
                    pedido.Pagoo = true;
                    _context.Entry(pedido).State = EntityState.Modified;
                }
                else
                {
                    return BadRequest("El pago con la tarjeta no fue exitoso.");
                }

                _context.Set<Pago>().Add(pago);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetPago), new { id = pago.PagoID }, pago);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // PUT: api/Pagos/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPago(int id, Pago pago)
        {
            if (id != pago.PagoID)
            {
                return BadRequest("El ID del pago no coincide.");
            }

            try
            {
                // Validación: Verifica que el Pedido y Tarjeta existen
                var pedido = await _context.Set<Pedido>().FindAsync(pago.PedidoID);
                if (pedido == null)
                {
                    return BadRequest("El pedido especificado no existe.");
                }

                var tarjeta = await _context.Set<Tarjeta>().FindAsync(pago.TarjetaID);
                if (tarjeta == null)
                {
                    return BadRequest("La tarjeta especificada no existe.");
                }

                // Actualiza el PrecioTotal desde el Pedido
                pago.PrecioTotal = pedido.PrecioTotal;

                // Validación del pago
                if (tarjeta.Pago)
                {
                    pago.Pagoo = true;
                    pedido.Pagoo = true;
                    _context.Entry(pedido).State = EntityState.Modified;
                }
                else
                {
                    return BadRequest("El pago con la tarjeta no fue exitoso.");
                }

                _context.Entry(pago).State = EntityState.Modified;
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
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }

            return NoContent();
        }

        // DELETE: api/Pagos/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePago(int id)
        {
            try
            {
                var pago = await _context.Set<Pago>().FindAsync(id);
                if (pago == null)
                {
                    return NotFound();
                }

                _context.Set<Pago>().Remove(pago);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        private bool PagoExists(int id)
        {
            return _context.Set<Pago>().Any(e => e.PagoID == id);
        }
    }
}