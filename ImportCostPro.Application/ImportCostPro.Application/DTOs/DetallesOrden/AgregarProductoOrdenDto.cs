using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportCostPro.Application.DTOs.DetallesOrden
{
    public class AgregarProductoOrdenDto
    {
        public int OrdenImportacionId { get; set; }
        public int ProductoId { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public decimal Cantidad { get; set; }
        public decimal PrecioUnitarioFOB { get; set; }
        public decimal MargenDeseado { get; set; }
    }
}

