using System.ComponentModel.DataAnnotations;
namespace FrescuraApi.Models
{
    public class Proveedores
    {
        [Key]
        public int ProveedoresID { get; set; }
        [StringLength(200)]
        public string NombreComercial { get; set; }
        [StringLength(100)]
        public string Contacto { get; set; }
        [StringLength(100)]
        public string Direccion { get; set; }
        public int TelefonoID { get; set; }
      
    }
}