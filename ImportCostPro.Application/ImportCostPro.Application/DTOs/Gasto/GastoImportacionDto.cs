using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using Persistance.Enums;

namespace ImportCostPro.Application.DTOs.Gasto
{
    // DTO para mostrar en la lista de la pantalla (Lectura)
    public class GastoImportacionDto
    {
        public int Id { get; set; }
        public int OrdenImportacionID { get; set; }

        public int MonedaID { get; set; }
        public string NombreMoneda { get; set; } = string.Empty; // Para que el usuario vea "Dólar" y no solo un "2"
        public string SimboloMoneda { get; set; } = string.Empty;

        public decimal Monto { get; set; }
        public DateTime FechaGasto { get; set; }

        // Asumiendo que tu compañero ya agregó TipoGasto a la entidad como le recomendamos
        public TipoGasto TipoGasto { get; set; }
        public MetodoDistribucion MetodoDistribucion { get; set; }
    }
}