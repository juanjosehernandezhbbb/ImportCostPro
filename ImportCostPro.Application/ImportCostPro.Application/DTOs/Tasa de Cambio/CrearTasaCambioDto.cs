using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportCostPro.Application.DTOs.TasasCambio
{
    public class CrearTasaCambioDto
    {
        public int MonedaOrigenID { get; set; }
        public int MonedaDestinoID { get; set; }
        public decimal ValorTasa { get; set; }
        public DateTime FechaVigencia { get; set; }
    }
}

