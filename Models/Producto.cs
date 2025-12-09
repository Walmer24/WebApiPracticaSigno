using System.ComponentModel.DataAnnotations.Schema;

namespace PracticaSigno.Models
{
    public class Producto
    {
        public int IdProducto { get; set; }
        public string? Descripcion { get; set; }
        public int Stock { get; set; }
        public decimal Precio { get; set; }
        public int IdCategoria { get; set; }
    }
}
