using Persistance.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportCostPro.Application.DTOs.Gasto
{
    public class AgregarGastoDto
    {
        public int OrdenImportacionID { get; set; }
        public int MonedaID { get; set; }
        public decimal Monto { get; set; }
        public DateTime FechaGasto { get; set; }

        public TipoGasto TipoGasto { get; set; }
        public MetodoDistribucion MetodoDistribucion { get; set; }
    }

}
