using Persistance.Enums;
using System.ComponentModel.DataAnnotations;

namespace ImportCostPro.Application.DTOs.Producto
{
    public class CrearProductoDto
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(150, ErrorMessage = "El nombre no puede superar 150 caracteres")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El código es obligatorio")]
        [StringLength(50, ErrorMessage = "El código no puede superar 50 caracteres")]
        public string Codigo { get; set; } = string.Empty;

        [Required(ErrorMessage = "Debe seleccionar el país de origen")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar el país de origen")]
        public int PaisOrigenID { get; set; }

        [Required(ErrorMessage = "Debe seleccionar la categoría arancelaria")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar la categoría arancelaria")]
        public int CategoriaArancelariaID { get; set; }

        [Required(ErrorMessage = "El peso unitario es obligatorio")]
        [Range(0.001, double.MaxValue, ErrorMessage = "El peso debe ser mayor a 0")]
        public decimal PesoUnitario { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "El largo debe ser mayor a 0")]
        public decimal? Largo { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "El ancho debe ser mayor a 0")]
        public decimal? Ancho { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "El alto debe ser mayor a 0")]
        public decimal? Alto { get; set; }

        [StringLength(500, ErrorMessage = "La descripción no puede superar 500 caracteres")]
        public string? Descripcion { get; set; }

        [Required(ErrorMessage = "Debe seleccionar la unidad de medida")]
        public UnidadMedida UnidadDeMedidaID { get; set; }
    }
}