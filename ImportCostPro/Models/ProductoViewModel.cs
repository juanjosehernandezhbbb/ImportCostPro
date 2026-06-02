using System.ComponentModel.DataAnnotations;

namespace ImportCostPro.Models
{
    public class ProductoViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El código de producto es obligatorio.")]
        [StringLength(50, ErrorMessage = "El código no puede exceder los 50 caracteres.")]
        [Display(Name = "Código SKU / Part Number")]
        public string Codigo { get; set; } = string.Empty;

        [Required(ErrorMessage = "El nombre del producto es obligatorio.")]
        [StringLength(150, ErrorMessage = "El nombre no puede exceder los 150 caracteres.")]
        [Display(Name = "Descripción del Producto")]
        public string Nombre { get; set; } = string.Empty;

        [StringLength(250, ErrorMessage = "La descripción detallada no puede exceder los 250 caracteres.")]
        [Display(Name = "Descripción Larga")]
        public string Descripcion { get; set; } = string.Empty;

        [Display(Name = "Estado")]
        public bool Activo { get; set; } = true;
    }
}