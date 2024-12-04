using System.ComponentModel.DataAnnotations;
namespace FrescuraApi.Models
{
    public class Inventario
    {
        [Key]
        public int InventarioID { get; set; }
        public int ProductoID { get; set; }
        public int PedidoID{ get; set; }
        public int SaveStock{ get; set; } // Stock minimo por producto
        public decimal PrecioCompra{ get; set; } // Precio de compra del producto
        public decimal TotalCompra{ get; set; } // Cantidad * PrecioCompra da el total
        public decimal PrecioVenta{ get; set; } // Precio al que se vedio
        public decimal TotalVenta{ get; set; } // Cantidad * PrecioVenta da el total de la venta
        public decimal Utilidad{ get; set; } // TotalVenta - TotalCompra Da como resultado la ganacia Bruta
        public DateTime FechaYHora{ get; set; } // Fecha y hora de la transaccion
        public bool IsCompleted { get; set; }
        // Relaciones
        public Producto? Producto { get; set; }
        public Pedido? Pedido { get; set; }
        
    }
}