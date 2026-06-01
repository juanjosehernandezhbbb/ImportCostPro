using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImportCostPro.Application.DTOs.Producto;

namespace ImportCostPro.Application.Interfaces
{
    public interface IProductoService
    {
        Task<IEnumerable<ProductoDto>> ObtenerTodosAsync();
        Task<ProductoDto> ObtenerPorIdAsync(int id);
        Task<int> CrearAsync(CrearProductoDto dto);
        Task ActualizarAsync(int id, CrearProductoDto dto);
        Task EliminarAsync(int id); // O inactivar lógicamente
    }
}
