using System.ComponentModel.DataAnnotations;
namespace FrescuraApi.Models
{
    public class Telefono
    {
        [Key]
        public int TelefonoID { get; set; }
        [StringLength(20)]
        public required string Telefono1 { get; set; }
        [StringLength(20)]
        public string? Telefono2 { get; set; }
        [StringLength(20)]
        public string? Telefono3 { get; set; }
        [StringLength(20)]
        public string? Telefono4 { get; set; }
        [StringLength(20)]
        public string? Telefono5 { get; set; }
        [StringLength(20)]
        public string? Telefono6 { get; set; }
        public bool IsCompleted { get; set; }
        // Claves foráneas
         public int? UsuarioID { get; set; } 
         public Usuarios? Usuario { get; set; } 
         public int? ProveedorID { get; set; } 
         public Proveedores? Proveedor { get; set; }
    }
}