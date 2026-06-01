using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImportCostPro.Application.DTOs.Ordenes;
using Persistance.Enums;

namespace ImportCostPro.Application.Interfaces
{
    public interface IOrdenImportacionService
    {
        // Métodos de consulta
        Task<IEnumerable<OrdenImportacionDto>> ObtenerTodasAsync();
        Task<OrdenImportacionDto> ObtenerPorIdAsync(int id);

        // Operaciones transaccionales
        Task<int> CrearAsync(CrearOrdenDto dto);

        // Actualización general (sólo permitida si la orden está 'Abierta')
        Task ActualizarAsync(int id, CrearOrdenDto dto);

        // Regla de Negocio: Borrado restringido si la orden tiene cálculo oficial
        Task EliminarAsync(int id);

        // Regla de Negocio: Cambio de estados controlado
        Task CambiarEstadoAsync(int id, EstadoOrden nuevoEstado);
    }
}
