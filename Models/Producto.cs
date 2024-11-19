using System.ComponentModel.DataAnnotations;
namespace FrescuraApi.Models
{
    public class Producto
    {
        [Key]
        public int ProductoID { get; set; }
        public required int Codigo { get; set; }
        [StringLength(80)]
        public required string NomgbreProducto { get; set; }
        [StringLength(100)]
        public required int Precio { get; set; }
        [StringLength(30)]
        public string? Peso { get; set; }
        // Informacion basica de un producto
    }
}