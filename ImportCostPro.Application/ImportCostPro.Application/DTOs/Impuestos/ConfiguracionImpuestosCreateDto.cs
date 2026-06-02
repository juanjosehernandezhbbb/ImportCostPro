using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportCostPro.Application.DTOs.Impuestos
{
    public class ConfiguracionImpuestosCreateDto
    {
        public decimal PorcentajeITBIS { get; set; }
        public decimal PorcentajeTasaServicioAduanal { get; set; }
    }
}
