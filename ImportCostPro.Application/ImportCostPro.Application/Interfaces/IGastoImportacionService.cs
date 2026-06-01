using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImportCostPro.Application.DTOs.Gasto;

namespace ImportCostPro.Application.Interfaces
{
    public interface IGastoImportacionService
    {
        // Traer todos los gastos de una orden específica para pintarlos en una tabla
        Task<IEnumerable<GastoImportacionDto>> ObtenerPorOrdenIdAsync(int ordenId);

        // Agregar un gasto validando que la orden esté abierta
        Task<int> AgregarGastoAsync(AgregarGastoDto dto);

        // Eliminar un gasto validando que la orden esté abierta
        Task EliminarGastoAsync(int gastoId);

        // Opcional: Actualizar un gasto si el usuario se equivocó en el monto
        Task ActualizarGastoAsync(int id, AgregarGastoDto dto);
    }
}