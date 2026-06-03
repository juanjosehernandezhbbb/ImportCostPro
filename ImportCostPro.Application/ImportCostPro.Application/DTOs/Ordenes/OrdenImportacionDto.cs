using Persistance.Enums;

namespace ImportCostPro.Application.DTOs.Ordenes
{
    public class OrdenImportacionDto
    {
        public int Id { get; set; }
        public string NumeroOrden { get; set; } = string.Empty;
        public int PaisOrigenID { get; set; }
        public string NombrePaisOrigen { get; set; } = string.Empty;
        public int MonedaID { get; set; }
        public string NombreMoneda { get; set; } = string.Empty;
        public int ImportadorID { get; set; }
        public string NombreImportador { get; set; } = string.Empty;
        public int ProveedorID { get; set; }
        public string NombreProveedor { get; set; } = string.Empty;
        public DateTime FechaOrden { get; set; }
        public ModalidadTransporte ModalidadTransporte { get; set; }

        public EstadoOrden Estado { get; set; }

        public decimal FobTotalCalculado { get; set; }
    }
}