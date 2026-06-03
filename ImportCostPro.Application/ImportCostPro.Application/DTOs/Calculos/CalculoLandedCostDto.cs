using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportCostPro.Application.DTOs.Calculos
{
    public class CalculoLandedCostDto
    {
        public int Id { get; set; }
        public string NumeroOrden { get; set; } = string.Empty;
        public DateTime FechaCalculo { get; set; }
        public decimal TotalCosto { get; set; }
    }
}