using Persistance.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Entities
{
    public class Pais: BasicEntity<int>
    {
        public required string Nombre { get; set; }
        public required string CodigoISO { get; set; }


        // Navigation property
        public ICollection<Importador> Importadores { get; set; }

    }
}
