using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportCostPro.Application.DTOs.Moneda
{
    public class ActualizarMonedaDTO
    {
        public int Id { get; set; }
        public string NombreMoneda { get; set; } = string.Empty;
        public string CodigoISO { get; set; } = string.Empty;
        public string Simbolo { get; set; } = string.Empty;
        public bool EsMonedaLocal { get; set; }
        public bool Estado { get; set; }
    }
}
