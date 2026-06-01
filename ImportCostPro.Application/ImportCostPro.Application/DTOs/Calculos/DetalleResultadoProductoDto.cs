using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportCostPro.Application.DTOs.Calculos
{
    public class DetalleResultadoProductoDto
    {
        public int ProductoId { get; set; }
        public string NombreProducto { get; set; } = string.Empty;
        public decimal Cantidad { get; set; }

        // Valores base
        public decimal PrecioFobUnitarioMonedaOrden { get; set; }
        public decimal PrecioFobTotalMonedaLocal { get; set; }

        // Prorrateo
        public decimal PorcentajeParticipacion { get; set; } // Qué tanto del gasto total le toca a este producto
        public decimal FleteAsignadoLocal { get; set; }
        public decimal SeguroAsignadoLocal { get; set; }

        // Fiscales y Totales
        public decimal ValorCifLocal { get; set; }
        public decimal ArancelAplicado { get; set; }
        public decimal ItbisAplicado { get; set; }

        // Resultados Finales
        public decimal CostoTotalImportadoLocal { get; set; }
        public decimal CostoUnitarioFinalLocal { get; set; }

        // Proyección de venta
        public decimal MargenGananciaDeseado { get; set; }
        public decimal PrecioVentaSugerido { get; set; }
    }

}
