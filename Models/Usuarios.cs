using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
namespace FrescuraApi.Models
{
    public class Usuarios
    {
        [Key]
        [NotNull]
        public int UsuarioID { get; set; }
        [StringLength(80)]
        public required string Tipo { get; set; }
        [StringLength(100)]
        public required string NombreUsuario { get; set; }
        [StringLength(100)]
        public string? Apellido { get; set; }
        [StringLength(100)]
        public required string Correo { get; set; }
        [StringLength(10)]
        public required string Contraseña { get; set; }
        [StringLength(200)]
        [Required]
        public required string Direccion { get; set; }
        public bool IsCompleted { get; set; }
        
        // Relaciones
        public ICollection<Telefono> Telefonos { get; set; } = new List<Telefono>();
        public ICollection<Pedido>? Pedidos { get; set; }
        public ICollection<Pago>? Pagos { get; set; }
        public ICollection<Tarjeta>? Tarjetas { get; set; }
    }
}