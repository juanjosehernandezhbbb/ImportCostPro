using System.ComponentModel.DataAnnotations;

namespace ImportCostPro.Models
{
    public class CategoriaArancelariaViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El código es obligatorio.")]
        public string Codigo { get; set; } = string.Empty;

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public string Nombre { get; set; } = string.Empty;

        [Range(0, 100, ErrorMessage = "El porcentaje debe estar entre 0 y 100.")]
        [Display(Name = "Porcentaje Arancel (%)")]
        public decimal PorcentajeArancel { get; set; }

        [Display(Name = "¿Aplica ITBIS?")]
        public bool AplicaITBIS { get; set; }

        [Display(Name = "¿Aplica Impuesto Selectivo?")]
        public bool AplicaImpuestoSelectivo { get; set; }

        [Display(Name = "Porcentaje Impuesto Selectivo (%)")]
        public decimal PorcentajeImpuestoSelectivo { get; set; }

        public bool Activo { get; set; } = true;
    }
}