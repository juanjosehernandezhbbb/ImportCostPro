using System.ComponentModel.DataAnnotations;

namespace ImportCostPro.Application.DTOs.TasasCambio
{
    public class CrearTasaCambioDto
    {
        [Required(ErrorMessage = "Debe seleccionar la moneda de origen")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar la moneda de origen")]
        public int MonedaOrigenID { get; set; }

        [Required(ErrorMessage = "Debe seleccionar la moneda de destino")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar la moneda de destino")]
        public int MonedaDestinoID { get; set; }

        [Required(ErrorMessage = "El valor de la tasa es obligatorio")]
        [Range(0.0001, double.MaxValue, ErrorMessage = "El valor debe ser mayor a 0")]
        public decimal ValorTasa { get; set; }

        [Required(ErrorMessage = "La fecha de vigencia es obligatoria")]
        public DateTime FechaVigencia { get; set; }
    }
}