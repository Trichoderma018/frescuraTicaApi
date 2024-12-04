using System.ComponentModel.DataAnnotations;
namespace FrescuraApi.Models
{
    public class Producto
    {
        [Key]
        public int ProductoID { get; set; }
        public required int Codigo { get; set; }
        [StringLength(80)]
        public required string NombreProducto { get; set; }
        [StringLength(100)]
        public required decimal Precio { get; set; }
        
        [StringLength(30)]
        public string? Peso { get; set; }
        public bool IsCompleted { get; set; }
        // Informacion basica de un producto

        // Relaciones
        public ICollection<Pedido>? Pedidos { get; set; }
        public ICollection<Inventario>? Inventarios { get; set; }
    }
}