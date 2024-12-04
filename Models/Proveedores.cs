using System.ComponentModel.DataAnnotations;
namespace FrescuraApi.Models
{
    public class Proveedores
    {
        [Key]
        public int ProveedoreID { get; set; }
        [StringLength(200)]
        public required string NombreComercial { get; set; }
        [StringLength(100)]
        [Required]
        public required string Contacto { get; set; }
        [StringLength(200)]
        [Required]
        public required string Direccion { get; set; }
        public bool IsCompleted { get; set; }
        // Relaciones
        public ICollection<Telefono> Telefonos { get; set; } = new List<Telefono>();
        public ICollection<Pedido>? Pedidos { get; set; }
    }
}