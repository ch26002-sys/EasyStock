using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace EasyStock.Models
{
    public class Producto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100)]
        [Display(Name = "Nombre del producto")]
        public string Nombre { get; set; } = string.Empty;

        [StringLength(250)]
        public string? Descripcion { get; set; }
[Required]
[Precision(18, 2)]
[Range(0.01, 999999.99, ErrorMessage = "El precio debe ser mayor que cero")]
public decimal Precio { get; set; }

        [Required]
        [Range(0, 100000)]
        public int Stock { get; set; }
    }
}