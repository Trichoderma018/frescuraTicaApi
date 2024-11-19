using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
namespace FrescuraApi.Models
{
    public class Usuarios
    {
        [Key]
        [NotNull]
        public int UsuariosID { get; set; }
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
        public int TelefonoID { get; set; } // La relacion con el modelo telefono es para agragar varios telefonos a un usuario
    }
}