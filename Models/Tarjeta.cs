using System.ComponentModel.DataAnnotations;
namespace FrescuraApi.Models
{
    public class Tarjeta
    {
        [Key]
        public int TarjetaID { get; set; }
        [StringLength(10)]
        public required string Cedula { get; set; }
        public required int NumeroTarjeta { get; set; } 
        public required DateOnly Expiracion { get; set; }
        public required int Cvv { get; set; } 
        public bool Pago { get; set; } // Es aqui donde se guarda si el pago fue exitoso o no y luego se envia a los modelos Pago y Pedido
        public int UsuarioID { get; set; }
    }
}