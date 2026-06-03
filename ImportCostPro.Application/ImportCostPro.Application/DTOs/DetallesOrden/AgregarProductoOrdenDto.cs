using System;
using System.ComponentModel.DataAnnotations;

namespace ImportCostPro.Application.DTOs.DetallesOrden
{
    public class AgregarProductoOrdenDto
    {
        // Este ID es indispensable para el proceso de Edición
        public int Id { get; set; }

        [Required]
        public int OrdenImportacionId { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un producto.")]
        public int ProductoId { get; set; }

        public string Descripcion { get; set; } = string.Empty;

        [Range(0.01, double.MaxValue, ErrorMessage = "La cantidad debe ser mayor a 0.")]
        public decimal Cantidad { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor a 0.")]
        public decimal PrecioUnitarioFOB { get; set; }

        public decimal MargenDeseado { get; set; }
    }
}