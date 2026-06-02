using System.ComponentModel.DataAnnotations;

namespace ImportCostPro.Models
{
    public class PaisViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del país es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres.")]
        [Display(Name = "Nombre del País")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El código ISO es obligatorio.")]
        [StringLength(3, MinimumLength = 2, ErrorMessage = "El código ISO debe tener entre 2 y 3 caracteres.")]
        [Display(Name = "Código ISO")]
        public string CodigoISO { get; set; } = string.Empty;

        [Display(Name = "Estado")]
        public bool Activo { get; set; } = true;
    }
}