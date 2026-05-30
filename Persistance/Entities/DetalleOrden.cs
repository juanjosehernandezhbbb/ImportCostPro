using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistance.Common;

namespace Persistance.Entities
{
    public class DetalleOrden : BasicEntity<int>
    {
            public int OrdenImportacionId { get; set; }
            public OrdenImportacion OrdenImportacion { get; set; }
            public required int ProductoId { get; set; }
            public Producto? Producto { get; set; }
            public required string Descripcion { get; set; }
        public required decimal Cantidad { get; set; }
            public required decimal PrecioUnitarioFOB { get; set; }
            public required decimal MargenDeseado { get; set; }
        
    }
}
