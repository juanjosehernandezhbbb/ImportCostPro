using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using Persistance.Enums; // Para usar ModalidadTransporte y EstadoOrden de tu compañero

namespace ImportCostPro.Application.DTOs.Ordenes
{
    // DTO para mostrar los datos (Lectura)
    public class OrdenImportacionDto
    {
        public int Id { get; set; }
        public string NumeroOrden { get; set; } = string.Empty;

        // Nombres descriptivos para las vistas (evitamos mandar solo el ID)
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

        // El FOB Total y el Total Estimado se calcularán dinámicamente o vendrán del LandedCost
        public decimal FobTotalCalculado { get; set; }
    }

}
