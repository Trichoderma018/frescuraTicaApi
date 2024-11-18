using System.ComponentModel.DataAnnotations;
namespace FrescuraApi.Models
{
    public class Telefono
    {
        [Key]
        public int TelefonoID { get; set; }
        [StringLength(20)]
        public string Telefono1 { get; set; }
        [StringLength(20)]
        public string Telefono2 { get; set; }
        [StringLength(20)]
        public string Telefono3 { get; set; }
        [StringLength(20)]
        public string Telefono4 { get; set; }
        [StringLength(20)]
        public string Telefono5 { get; set; }
        [StringLength(20)]
        public string Telefono6 { get; set; }
        public int UsuariosID { get; set; }
        public int ProveedoresID { get; set; }
    }
}