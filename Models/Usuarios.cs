using System.ComponentModel.DataAnnotations;
namespace FrescuraApi.Models
{
    public class Usuarios
    {
        [Key]
        public int UsuariosID { get; set; }
        [StringLength(80)]
        public string Tipo { get; set; }
        [StringLength(100)]
        public string NombreUsuario { get; set; }
        [StringLength(100)]
        public string Apellido { get; set; }
        [StringLength(100)]
        public string Correo { get; set; }
        [StringLength(10)]
        public string Contraseña { get; set; }
        public int TelefonoID { get; set; }
    }
}