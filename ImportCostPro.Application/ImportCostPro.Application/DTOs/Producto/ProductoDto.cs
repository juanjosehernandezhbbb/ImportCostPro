using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistance.Enums; // Para UnidadMedida

namespace ImportCostPro.Application.DTOs.Producto
{
    public class ProductoDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Codigo { get; set; } = string.Empty;

        public int PaisOrigenID { get; set; }
        public string NombrePaisOrigen { get; set; } = string.Empty;

        public int CategoriaArancelariaID { get; set; }
        // Aquí puedes poner el nombre de la categoría o su porcentaje, ej: "Ropa (20%)"
        public string DescripcionCategoria { get; set; } = string.Empty;

        public decimal PesoUnitario { get; set; }
        public decimal? Largo { get; set; }
        public decimal? Ancho { get; set; }
        public decimal? Alto { get; set; }
        public string? Descripcion { get; set; }
        public UnidadMedida UnidadDeMedidaID { get; set; }
        public bool Estado { get; set; }
    }
}
