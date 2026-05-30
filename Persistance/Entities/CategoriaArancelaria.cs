using Persistance.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Entities
{
    public class CategoriaArancelaria:BasicEntity<int>
    {
        public required string CodigoArancelio { get; set; }
        public required string Nombre { get; set; }
        public required decimal PorcentajeArancel { get; set; }
        public required bool AplicaITBS { get; set; }
        public required bool AplicaSelectivo { get; set; }
        public decimal? PorctImptSelectivo { get; set; }
       
    }
}
