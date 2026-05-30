using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistance.Common;

namespace Persistance.Entities
{
    public class TasaCambio : BasicEntity<int>
    {
        public required int MonedaOrigenID { get; set; }//fk
        public Moneda? MonedaOrigen { get; set; }
        public required int MonedaDestinoID { get; set; }//fk
        public Moneda? MonedaDestino { get; set; }
        public required decimal ValorTasa { get; set; }
        public required DateTime FechaVigencia { get; set; }


    }
}
