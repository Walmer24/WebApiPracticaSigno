using System.ComponentModel.DataAnnotations.Schema;

namespace PracticaSigno.Models
{
    public class Categoria
    {
        public int IdCategoria { get; set; }
        public string? Descripcion { get; set; }
    }
}
