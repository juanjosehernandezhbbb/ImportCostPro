using Persistance.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportCostPro.Application.DTOs.Ordenes
{
    public class CrearOrdenDto
    {
        public string NumeroOrden { get; set; } = string.Empty;
        public int PaisOrigenID { get; set; }
        public int MonedaID { get; set; }
        public int ImportadorID { get; set; }
        public int ProveedorID { get; set; }
        public DateTime FechaOrden { get; set; }
        public ModalidadTransporte ModalidadTransporte { get; set; }

        public EstadoOrden EstadoOrden { get; set; }

        // NOTA: No pedimos Estado aquí, porque la regla de negocio dice que 
        // TODA orden nace como "Abierta" automáticamente.
    }
}
