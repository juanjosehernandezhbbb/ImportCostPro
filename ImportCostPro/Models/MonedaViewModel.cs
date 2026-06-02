using System.ComponentModel.DataAnnotations;

namespace ImportCostPro.Models
{
    public class MonedaViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre de la moneda es obligatorio.")]
        [StringLength(50, ErrorMessage = "El nombre no puede exceder los 50 caracteres.")]
        [Display(Name = "Nombre de la Moneda")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El código ISO es obligatorio.")]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "El código debe tener exactamente 3 caracteres (ej. USD).")]
        [Display(Name = "Código ISO")]
        public string CodigoISO { get; set; } = string.Empty;

        [Required(ErrorMessage = "El símbolo es obligatorio.")]
        [StringLength(5, ErrorMessage = "El símbolo no puede exceder los 5 caracteres.")]
        [Display(Name = "Símbolo")]
        public string Simbolo { get; set; } = string.Empty;

        [Display(Name = "¿Es Moneda Local?")]
        public bool EsMonedaLocal { get; set; } 

        public bool Activo { get; set; } = true;
    }
}