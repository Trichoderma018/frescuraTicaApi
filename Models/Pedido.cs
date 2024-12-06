using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace FrescuraApi.Models
{
    public class Pedido
    {
        [Key]
        public int PedidoID { get; set; }
        public int ProductoID { get; set; } // Para traer información del modelo Producto
        [NotNull]
        public required int Cantidad { get; set; } // Cantidad de productos que se van a comprar
        [NotNull]
        public required int PrecioTotal { get; set; } // PrecioTotal = Cantidad * ProductoID.Precio
        [StringLength(10)]
        public required string TipoMovimiento { get; set; } // Entrada (Proveedor) o salida (Usuario)
        public bool Pagoo { get; set; } // Aquí llega la validación de que el pago sea exitoso o no
        public bool IsCompleted { get; set; }
        public int ProveedorID { get; set; } // Para traer información del modelo Proveedores
        public int UsuarioID { get; set; } // Para traer información del modelo Usuarios
        public Producto? Producto { get; set; }
        public Usuarios? Usuario { get; set; }
        public Proveedores? Proveedor { get; set; }
        public ICollection<Inventario>? Inventarios { get; set; }
        public ICollection<Pago>? Pagos { get; set; }
    }
}