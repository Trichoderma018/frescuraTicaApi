using System.ComponentModel.DataAnnotations;

namespace FrescuraApi.Models
{
    public class Tarjeta
    {
        [Key]
        public int TarjetaID { get; set; }

        [StringLength(10)]
        public required string Cedula { get; set; }

        public required string NumeroTarjeta { get; set; } // Cambiado a string para manejar caracteres especiales

        public required DateOnly Expiracion { get; set; }

        public required int Cvv { get; set; }

        public bool Pago { get; set; } // Es aquí donde se genera la validación de pago fue exitoso o no y luego se envía a los modelos Pago y Pedido

        public bool IsCompleted { get; set; }

        public int UsuarioID { get; set; }

        // Relaciones
        public Usuarios? Usuarios { get; set; }

        public ICollection<Pago>? Pagos { get; set; }
    }
}