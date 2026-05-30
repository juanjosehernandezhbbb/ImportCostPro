using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistance.Common;
using Persistance.Enums;
using Persistance.Entities;


namespace Persistance.Entities
{
    public class GastoImportacion: BasicEntity<int>
    {
        public required int OrdenImportacionID { get; set; }//fk
        //public Importador OrdenImportacion { get; set; }
        public required decimal Monto { get; set; }
        public required DateTime FechaGasto { get; set; }
        public required int MonedaID { get; set; }//fk
        public Moneda Moneda { get; set; }
        public OrdenImportacion? OrdenImportacion { get; set; }
        public MetodoDistribucion MetodoDistribucion { get; set; }
    }
}
