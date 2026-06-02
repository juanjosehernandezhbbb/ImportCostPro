using System.ComponentModel.DataAnnotations;

namespace ImportCostPro.Models
{
    public class ImportadorViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El RNC es obligatorio.")]
        [StringLength(11, MinimumLength = 9, ErrorMessage = "El RNC debe tener entre 9 y 11 caracteres.")]
        [Display(Name = "RNC / Identificación")]
        public string Rnc { get; set; } = string.Empty;

        [Required(ErrorMessage = "La razón social es obligatoria.")]
        [StringLength(150, ErrorMessage = "La razón social no puede exceder los 150 caracteres.")]
        [Display(Name = "Razón Social")]
        public string RazonSocial { get; set; } = string.Empty;

        [Required(ErrorMessage = "El teléfono es obligatorio.")]
        [StringLength(20, ErrorMessage = "El teléfono no puede exceder los 20 caracteres.")]
        [Display(Name = "Teléfono")]
        public string Telefono { get; set; } = string.Empty;

        [StringLength(100, ErrorMessage = "El nombre del contacto no puede exceder los 100 caracteres.")]
        [Display(Name = "Contacto Principal")]
        public string Contacto { get; set; } = string.Empty;
    }
}