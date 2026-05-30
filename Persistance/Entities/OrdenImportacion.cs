using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistance.Common;
using Persistance.Enums;

namespace Persistance.Entities
{
    public class OrdenImportacion : BasicEntity<int>
    {
        public required string NumeroOrden { get; set; }
        public required int PaisOrigenID { get; set; }
        public Pais? PaisOrigen { get; set; }
        public required int MonedaID { get; set; }
        public Moneda? Moneda { get; set; }
        public required int ImportadorID { get; set; }
        public Importador? Importador { get; set; }
        public int ProveedorID { get; set; }
        public Proveedor? Proveedor { get; set; }
        public ResultadoCalculo ResultadoCalculo { get; set; }
        public required DateTime FechaOrden { get; set; }
        public ICollection<DetalleOrden>? DetallesOrden { get; set; }
        public ICollection<GastoImportacion>? GastosImportacion { get; set; }
        public ModalidadTransporte modalidadTransporte { get; set; }
        public EstadoOrden estado { get; set; }
    }

}
