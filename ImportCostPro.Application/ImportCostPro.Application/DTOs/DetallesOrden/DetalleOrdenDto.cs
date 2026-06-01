using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportCostPro.Application.DTOs.DetallesOrden
{
    // DTO para enviar datos a la Vista (Lectura)
    public class DetalleOrdenDto
    {
        public int Id { get; set; }
        public int OrdenImportacionId { get; set; }

        public int ProductoId { get; set; }
        public string NombreProducto { get; set; } = string.Empty; // Útil para que la vista no muestre solo un número

        public string Descripcion { get; set; } = string.Empty;
        public decimal Cantidad { get; set; }
        public decimal PrecioUnitarioFOB { get; set; }
        public decimal MargenDeseado { get; set; }

        // Propiedad calculada útil para mostrar en la tabla de la vista
        public decimal FobTotal => Cantidad * PrecioUnitarioFOB;
    }
}