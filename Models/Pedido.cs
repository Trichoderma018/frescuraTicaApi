using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
namespace FrescuraApi.Models
{
    public class Pedido
    {
        [Key]
        public int PedidoID { get; set; }
        [NotNull]
        public required int Cantidad { get; set; } // Cantidad de productos que se van a comprar
        [NotNull]
        public required int PrecioTotal { get; set; } // PrecioTotal = Cantidad * ProductoID.Precio
        [StringLength(10)]
        public required string TipoMovimiento { get; set; } // Entrada (Proveedor) o salida (Usuario)
        public bool Pago { get; set; } // Aqui llega la validacion de que el pago sea exitoso o no
        public int ProductoID { get; set; } // Para traer informacion del modelo Producto
        public int ProveedoresID { get; set; } // Para traer informacion del modelo Proveedores
        public int UsuarioID { get; set; } // Para traer informacion del modelo Usuarios
    }
}