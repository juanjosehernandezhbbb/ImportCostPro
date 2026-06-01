using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ImportCostPro.Application.DTOs.Calculos;

namespace ImportCostPro.Application.Interfaces
{
    public interface ILandedCostService
    {
        // 1. Ejecuta los 15 pasos en memoria y devuelve el resultado, pero NO lo guarda en base de datos.
        // Sirve para que el usuario vea cómo van los números (modo borrador).
        Task<ResultadoCalculoDto> PrevisualizarCalculoAsync(int ordenId);

        // 2. Ejecuta el cálculo, guarda la "fotografía inmutable" en la base de datos (tablas ResultadoCalculo y DetalleCalculo)
        // y cambia el estado de la orden a "Calculada".
        Task<int> GuardarCalculoOficialAsync(int ordenId);
    }
}
