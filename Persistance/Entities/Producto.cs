using Persistance.Common;
using Persistance.Enums;

namespace Persistance.Entities
{
    public class Producto : BasicEntity<int>
    {
        public string Nombre { get; set; } = string.Empty;
        public string Codigo { get; set; } = string.Empty;
        public int PaisOrigenID { get; set; }
        public Pais? PaisOrigen { get; set; }
        public int CategoriaArancelariaID { get; set; }
        public CategoriaArancelaria? CategoriaArancelaria { get; set; }
        public decimal PesoUnitario { get; set; }
        public decimal? Largo { get; set; }
        public decimal? Ancho { get; set; }
        public decimal? Alto { get; set; }
        public string? Descripcion { get; set; }
        public UnidadMedida UnidadDeMedidaID { get; set; }
    }
}