using Persistance.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportCostPro.Application.DTOs.Producto
{
    public class CrearProductoDto
    {
        public string Nombre { get; set; } = string.Empty;
        public string Codigo { get; set; } = string.Empty;
        public int PaisOrigenID { get; set; }
        public int CategoriaArancelariaID { get; set; }
        public decimal PesoUnitario { get; set; }
        public decimal? Largo { get; set; }
        public decimal? Ancho { get; set; }
        public decimal? Alto { get; set; }
        public string? Descripcion { get; set; }
        public UnidadMedida UnidadDeMedidaID { get; set; }
    }
}

