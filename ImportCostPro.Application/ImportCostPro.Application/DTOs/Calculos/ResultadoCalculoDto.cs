using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ImportCostPro.Application.DTOs.Calculos
{
    // Este es el "Resumen General" que se mostrará en la parte superior de la pantalla
    public class ResultadoCalculoDto
    {
        public int OrdenImportacionId { get; set; }
        public string NumeroOrden { get; set; } = string.Empty;
        public DateTime FechaCalculo { get; set; }

        public decimal TotalFobConvertidoLocal { get; set; }
        public decimal TotalFleteLocal { get; set; }
        public decimal TotalSeguroLocal { get; set; }
        public decimal TotalCifLocal { get; set; }
        public decimal TotalAranceles { get; set; }
        public decimal TotalItbis { get; set; }
        public decimal GranTotalLandedCost { get; set; }

        // La lista con el desglose exacto de cada producto
        public List<DetalleResultadoProductoDto> DetallesPorProducto { get; set; } = new List<DetalleResultadoProductoDto>();
    }

}
