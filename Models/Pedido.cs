using System.ComponentModel.DataAnnotations;
namespace FrescuraApi.Models
{
    public class Pedido
    {
        [Key]
        public int PedidoID { get; set; }
        public required int Cantidad { get; set; }
        public required int PrecioTotal { get; set; } // PrecioTotal = Cantidad * ProductoID.Precio
        [StringLength(10)]
        public required string TipoMovimiento { get; set; } // Entrada o salida
        public bool Pago { get; set; } // true o false
        public int ProductoID { get; set; }
        public int ProveedoresID { get; set; }
    }
}