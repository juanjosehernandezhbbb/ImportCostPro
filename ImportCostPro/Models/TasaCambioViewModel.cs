using System.ComponentModel.DataAnnotations;

namespace ImportCostPro.Models
{
    public class TasaCambioViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe seleccionar la moneda de origen.")]
        [Display(Name = "Moneda Origen")]
        public int MonedaOrigenId { get; set; }

        [Display(Name = "Moneda Origen")]
        public string CodigoMonedaOrigen { get; set; } = string.Empty;

        [Required(ErrorMessage = "Debe seleccionar la moneda de destino.")]
        [Display(Name = "Moneda Destino")]
        public int MonedaDestinoId { get; set; }

        [Display(Name = "Moneda Destino")]
        public string CodigoMonedaDestino { get; set; } = string.Empty;

        [Required(ErrorMessage = "El valor de la tasa es obligatorio.")]
        [Range(0.0001, 1000000, ErrorMessage = "La tasa debe ser un valor mayor a cero.")]
        [Display(Name = "Tasa de Cambio")]
        public decimal ValorTasa { get; set; }

        [Required(ErrorMessage = "La fecha de efectividad es obligatoria.")]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Tasa")]
        public DateTime Fecha { get; set; } = DateTime.Today;
    }
}