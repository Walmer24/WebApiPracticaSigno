using System.ComponentModel.DataAnnotations.Schema;

namespace PracticaSigno.Models
{
    public class Venta
    {
        public int IdVenta { get; set; }
        public DateTime Fecha { get; set; }
    }
}
