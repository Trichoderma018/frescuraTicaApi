using Microsoft.EntityFrameworkCore;

namespace FrescuraApi.Models
{
    public class FreshContext : DbContext
    {
        public FreshContext(DbContextOptions<FreshContext> options) : base(options) { }

        public required DbSet<Usuarios> Usuarios { get; set; }
        public required DbSet<Proveedores> Proveedores { get; set; }
        public required DbSet<Telefono> Telefono { get; set; }
        public required DbSet<Tarjeta> Tarjeta { get; set; }
        public required DbSet<Pago> Pago { get; set; }
        public required DbSet<Pedido> Pedido { get; set; }
        public required DbSet<Inventario> Inventario { get; set; }
        public required DbSet<Producto> Producto { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuarios>().HasKey(u => u.UsuarioID);
            modelBuilder.Entity<Proveedores>().HasKey(p => p.ProveedoreID);
            modelBuilder.Entity<Telefono>().HasKey(t => t.TelefonoID);

            // Configurar relaciones
            // Relacion de Telefono con Usuario
            modelBuilder.Entity<Telefono>()
                .HasOne(t => t.Usuario)
                .WithMany(u => u.Telefonos)
                .HasForeignKey(t => t.UsuarioID)
                .OnDelete(DeleteBehavior.Restrict);
            
            // Relacion de Telefono con Proveedor
            modelBuilder.Entity<Telefono>()
                .HasOne(t => t.Proveedor)
                .WithMany(p => p.Telefonos)
                .HasForeignKey(t => t.ProveedorID)
                .OnDelete(DeleteBehavior.Restrict);
            
            // Relacion de Usuario con Pedido
            modelBuilder.Entity<Pedido>()
                .HasOne(p => p.Usuario)
                .WithMany(u => u.Pedidos)
                .HasForeignKey(p => p.UsuarioID)
                .OnDelete(DeleteBehavior.Restrict);
            // Relacion de Proveedor con Pedido
            modelBuilder.Entity<Pedido>()
                .HasOne(p => p.Proveedor)
                .WithMany(pr => pr.Pedidos)
                .HasForeignKey(p => p.ProveedorID)
                .OnDelete(DeleteBehavior.Restrict);
            // Relacion de Producto con Pedido
            modelBuilder.Entity<Pedido>()
                .HasOne(p => p.Producto)
                .WithMany(pr => pr.Pedidos)
                .HasForeignKey(p => p.ProductoID)
                .OnDelete(DeleteBehavior.Restrict);
            // Relacion de Producto con Inventario
            modelBuilder.Entity<Inventario>()
                .HasOne(i => i.Producto)
                .WithMany(p => p.Inventarios)
                .HasForeignKey(i => i.ProductoID)
                .OnDelete(DeleteBehavior.Restrict);
            // Relacion de Pedido con Inventario
            modelBuilder.Entity<Inventario>()
                .HasOne(i => i.Pedido)
                .WithMany(p => p.Inventarios)
                .HasForeignKey(i => i.PedidoID)
                .OnDelete(DeleteBehavior.Restrict);
            // Relacion de Pedido con Pago
            modelBuilder.Entity<Pago>()
                .HasOne(p => p.Pedido)
                .WithMany(ped => ped.Pagos)
                .HasForeignKey(p => p.PedidoID)
                .OnDelete(DeleteBehavior.Restrict);
            // Relacion de Tarjeta con Pago
            modelBuilder.Entity<Pago>()
                .HasOne(p => p.Tarjeta)
                .WithMany(t => t.Pagos)
                .HasForeignKey(p => p.TarjetaID)
                .OnDelete(DeleteBehavior.Restrict);
            // Relacion de Usuario con Pago
            modelBuilder.Entity<Pago>()
                .HasOne(p => p.Usuarios)
                .WithMany(u => u.Pagos)
                .HasForeignKey(p => p.UsuarioID)
                .OnDelete(DeleteBehavior.Restrict);
            // Relacion de Tarjeta con Usuario
            modelBuilder.Entity<Tarjeta>()
                .HasOne(t => t.Usuarios)
                .WithMany(u => u.Tarjetas)
                .HasForeignKey(t => t.UsuarioID)
                .OnDelete(DeleteBehavior.Restrict);
            // Relacion de Tarjeta con Pago
            modelBuilder.Entity<Tarjeta>()
                .HasMany(t => t.Pagos)
                .WithOne(p => p.Tarjeta)
                .HasForeignKey(p => p.TarjetaID)
                .OnDelete(DeleteBehavior.Restrict);

            // Configurar tipos de columna para propiedades decimales
            modelBuilder.Entity<Inventario>()
                .Property(i => i.PrecioCompra)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Inventario>()
                .Property(i => i.PrecioVenta)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Inventario>()
                .Property(i => i.TotalCompra)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Inventario>()
                .Property(i => i.TotalVenta)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Inventario>()
                .Property(i => i.Utilidad)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Pago>()
                .Property(p => p.PrecioTotal)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Producto>()
                .Property(p => p.Precio)
                .HasColumnType("decimal(18,2)");
        }
        
    }
}
