using System;
using System.ComponentModel.DataAnnotations.Schema;
using Persistance.Common;
using Persistance.Enums;

namespace Persistance.Entities
{
    public class GastoImportacion : BasicEntity<int>
    {
        public int OrdenImportacionId { get; set; }

        [ForeignKey(nameof(OrdenImportacionId))] // Usamos nameof para evitar errores de escritura
        public OrdenImportacion? OrdenImportacion { get; set; }

        public decimal Monto { get; set; }
        public DateTime FechaGasto { get; set; }

        public int MonedaID { get; set; }

        [ForeignKey("MonedaID")]
        public Moneda? Moneda { get; set; }

        public MetodoDistribucion MetodoDistribucion { get; set; }
        public TipoGasto TipoGasto { get; set; }

        public bool Estado { get; set; }
    }
}