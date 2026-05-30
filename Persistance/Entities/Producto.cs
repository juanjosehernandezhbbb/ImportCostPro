using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistance.Common;
using Persistance.Enums;

namespace Persistance.Entities
{
    public class Producto: BasicEntity<int>
    {
        public required string Nombre { get; set; }
        public required string Codigo { get; set; }
        public required int PaisOrigenID { get; set; }//fk
        public Pais PaisOrigen { get; set; }
        public required int CategoriaArancelariaID { get; set; }//fk
        public CategoriaArancelaria CategoriaArancelaria { get; set; }
        public required decimal PesoUnitario { get; set; }
        public decimal? Largo { get; set; }
        public decimal? Ancho { get; set; }
        public decimal? Alto { get; set; }
        public string? Descripcion { get; set; }
        public required UnidadMedida UnidadDeMedidaID { get; set; }//fk

    }
}
