using System.ComponentModel.DataAnnotations;

namespace ImportCostPro.Models
{
    public class ImpuestoViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El porcentaje de ITBIS es obligatorio.")]
        [Range(0, 100, ErrorMessage = "El porcentaje debe estar entre 0% y 100%.")]
        [Display(Name = "Porcentaje de ITBIS (%)")]
        public decimal PorcentajeITBIS { get; set; }

        [Required(ErrorMessage = "La tasa de servicio aduanal es obligatoria.")]
        [Range(0, 100, ErrorMessage = "La tasa debe estar entre 0% y 100%.")]
        [Display(Name = "Tasa Servicio Aduanal (%)")]
        public decimal PorcentajeTasaServicioAduanal { get; set; }
    }
}