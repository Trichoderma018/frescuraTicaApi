using System.ComponentModel.DataAnnotations;
namespace FrescuraApi.Models
{
    public class Inventario
    {
        [Key]
        public int InventarioID { get; set; }
        public int SaveStock{ get; set; } // Stock minimo por producto
        public int PrecioCompra{ get; set; } // Precio de compra del producto
        public int TotalCompra{ get; set; } // Cantidad * PrecioCompra da el total
        public int PrecioVenta{ get; set; } // Precio al que se vedio
        public int TotalVenta{ get; set; } // Cantidad * PrecioVenta da el total de la venta
        public int Utilidad{ get; set; } // TotalVenta - TotalCompra Da como resultado la ganacia Bruta
        public DateTime FechaYHora{ get; set; } // Fecha y hora de la transaccion
        public int ProductoID { get; set; }
        public int PedidoID{ get; set; }
        
    }
}