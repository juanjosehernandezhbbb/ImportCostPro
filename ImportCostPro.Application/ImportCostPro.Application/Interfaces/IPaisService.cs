using ImportCostPro.Application.DTOs.Pais;
using ImportCostPro.Application.DTOs.Pais.ImportCostPro.Application.DTOs.Pais;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportCostPro.Application.Interfaces
{
    public interface IPaisService
    {
        Task<IEnumerable<PaisDto>> ObtenerTodosAsync();
        Task<PaisDto> ObtenerPorIdAsync(int id);
        Task<int> CrearAsync(CrearPaisDto dto);
        Task ActualizarAsync(int id, CrearPaisDto dto);
        Task EliminarAsync(int id);
    }
}