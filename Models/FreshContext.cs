using Microsoft.EntityFrameworkCore;

namespace FrescuraApi.Models
{
    public class FreshContext(DbContextOptions<FreshContext> options) : DbContext(options)
    {
        public required DbSet<Usuarios> Usuarios { get; set; }
        public required DbSet<Proveedores> Proveedores { get; set; }
        public required DbSet<Telefono> Telefono { get; set; }
        public required DbSet<Tarjeta> Tarjeta { get; set; }
        public required DbSet<Pago> Pago { get; set; }
        public required DbSet<Pedido> Pedido { get; set; }
        public required DbSet<Inventario> Inventario { get; set; }
        public required DbSet<Producto> Producto { get; set; }
    }
}