using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ImportCostPro.Application.DTOs.Proveedores;

namespace ImportCostPro.Application.Interfaces
{
    public interface IProveedorService
    {
        Task<IEnumerable<ProveedorDto>> ObtenerTodosAsync();
        Task<ProveedorDto> ObtenerPorIdAsync(int id);
        Task<int> CrearAsync(CrearProveedorDto dto);
        Task ActualizarAsync(int id, CrearProveedorDto dto);
        Task EliminarAsync(int id);
    }
}
