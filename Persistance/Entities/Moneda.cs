using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistance.Common;

namespace Persistance.Entities
{
    public class Moneda : BasicEntity<int>
    {
        public required string Nombre { get; set; }
        public required string CodigoIso { get; set; }
        public required string Simbolo { get; set; }
        public bool? EsMonedaLocal { get; set; }
        public ICollection<TasaCambio> TasaCambio { get; set; }

    }
}
