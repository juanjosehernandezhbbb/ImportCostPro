using Persistance.Enums;

namespace ImportCostPro.Application.DTOs.Gasto
{
    public class GastoImportacionDto
    {
        public int Id { get; set; }
        public int OrdenImportacionId { get; set; }
        public string NumeroOrden { get; set; } = string.Empty;

        public int MonedaID { get; set; }
        public string NombreMoneda { get; set; } = string.Empty;
        public string SimboloMoneda { get; set; } = string.Empty;
        public decimal Monto { get; set; }
        public DateTime FechaGasto { get; set; }
        public TipoGasto TipoGasto { get; set; }
        public MetodoDistribucion MetodoDistribucion { get; set; }
        public bool Estado { get; set; }
    }
}