using System.ComponentModel.DataAnnotations;
namespace FrescuraApi.Models
{
    public class Proveedores
    {
        [Key]
        public int ProveedoresID { get; set; }
        [StringLength(200)]
        public required string NombreComercial { get; set; }
        [StringLength(100)]
        [Required]
        public required string Contacto { get; set; }
        [StringLength(200)]
        [Required]
        public required string Direccion { get; set; }
        public int TelefonoID { get; set; } // La relacion con el modelo telefono es para agragar varios telefonos a un proveedor
      
    }
}