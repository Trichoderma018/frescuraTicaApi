using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
namespace FrescuraApi.Models
{
    public class Pago
    {
        [Key]
        public int PagoID { get; set; }
        [StringLength(100)]
        public string? Detalle { get; set; }
        public required int PrecioTotal { get; set; } // Referencias a precioTotal del modelo Pedido
        public bool Pagoo { get; set; } // Este es un paso intermedio para que llegue la validacion de tarjeta a pedido
        public int TarjetaID { get; set; } // Relacion para agregar tarjeta al pago
        public int PedidoID { get; set; } // Esta es la relacion con el Pedido
        public int UsuarioID { get; set; } // Esta es la relacion con el Usuario que esta comprando
    }
}