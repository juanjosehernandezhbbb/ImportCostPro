using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Entities
{
    public class DetalleCalculo
    {
        public int Id { get; set; }
        public int ResultadoCalculoId { get; set; }
        public required ResultadoCalculo ResultadoCalculo { get; set; }
        public int ProductoId { get; set; }
        public required Producto Producto { get; set; }
        public required decimal CostoFOB { get; set; }
        public required decimal Arancel { get; set; }
        public required decimal ITBIS { get; set; }
        public required decimal CostoTotal { get; set; }
    }
}
