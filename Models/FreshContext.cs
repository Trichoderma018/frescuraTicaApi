namespace FrescuraApi.Models
{
    public class FreshContext(DbContextOptions<FreshContext> options) : DbContext(options)
    {
        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<Proveedores> Proveedores { get; set; }
        public DbSet<Telefono> Telefono { get; set; }
    }
}