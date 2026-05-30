using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistance.Common;

namespace Persistance.Entities
{
    public class ResultadoCalculo: BasicEntity<int>
    {
            public required int OrdenImportacionId { get; set; }
            public OrdenImportacion OrdenImportacion { get; set; }
            public required int MonedaLocalId { get; set; }
            public Moneda MonedaLocal { get; set; }
            public int? TasaCambioUsadaId { get; set; }
            public TasaCambio? TasaCambioUsada { get; set; }
            public decimal FOBTotalOriginal { get; set; }
            public decimal FOBTotalLocal { get; set; }
            public decimal FleteTotal { get; set; }
            public decimal SeguroTotal { get; set; }
            public decimal CIFTotal { get; set; }
            public decimal TotalArancel { get; set; }
            public decimal TotalITBIS { get; set; }
            public decimal CostoTotalImportacion { get; set; }
            public decimal PorcentajeITBIS { get; set; }
            public decimal PorcentajeTasaAduanal { get; set; }
            public DateTime FechaCalculo { get; set; }

        //navegation property
        public ICollection<DetalleCalculo>? DetallesResultadoCalculo { get; set; }



    }
}
