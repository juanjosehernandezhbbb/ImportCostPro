using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportCostPro.Application.DTOs.TasasCambio
{
    public class TasaCambioDto
    {
        public int Id { get; set; }
        public int MonedaOrigenID { get; set; }
        public string NombreMonedaOrigen { get; set; } = string.Empty;
        public int MonedaDestinoID { get; set; }
        public string NombreMonedaDestino { get; set; } = string.Empty;
        public decimal ValorTasa { get; set; }
        public DateTime FechaVigencia { get; set; }
        public bool Estado { get; set; }
    }
}
