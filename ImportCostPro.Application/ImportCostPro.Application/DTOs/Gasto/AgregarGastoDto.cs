using Persistance.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace ImportCostPro.Application.DTOs.Gasto
{
    public class AgregarGastoDto
    {
        public int OrdenImportacionId { get; set; }

        [Required(ErrorMessage = "La moneda es obligatoria.")]
        public int MonedaID { get; set; }

        [Required(ErrorMessage = "El monto es obligatorio.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El monto debe ser mayor a cero.")]
        public decimal Monto { get; set; }

        [Required(ErrorMessage = "La fecha es obligatoria.")]
        public DateTime FechaGasto { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un tipo de gasto.")]
        public TipoGasto TipoGasto { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un método de distribución.")]
        public MetodoDistribucion MetodoDistribucion { get; set; }

        public bool Estado { get; set; } = true;
    }
}