using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ImportCostPro.Application.DTOs.DetallesOrden;
using ImportCostPro.Application.DTOs.Producto;

namespace ImportCostPro.Application.Interfaces
{
    public interface IDetalleOrdenService
    {
        // Trae todos los productos que están dentro de una orden específica
        Task<IEnumerable<DetalleOrdenDto>> ObtenerPorOrdenIdAsync(int ordenId);

        // Operaciones para manipular la lista de productos
        Task<int> AgregarProductoAsync(AgregarProductoOrdenDto dto);
        Task EliminarProductoAsync(int detalleId);
    }
}
