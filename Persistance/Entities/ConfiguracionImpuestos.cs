using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Entities
{
    public class ConfiguracionImpuestos
    {
        public int Id { get; set; }
        public required decimal PorcentajeITBIS { get; set; }
        public required decimal PorcentajeTasaAduanal { get; set; }
    }
}
