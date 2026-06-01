using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ImportCostPro.Application.DTOs.Importadores;

namespace ImportCostPro.Application.Interfaces
{
    public interface IImportadorService
    {
        Task<IEnumerable<ImportadorDto>> ObtenerTodosAsync();
        Task<ImportadorDto> ObtenerPorIdAsync(int id);
        Task<int> CrearAsync(CrearImportadorDto dto);
        Task ActualizarAsync(int id, CrearImportadorDto dto);
        Task EliminarAsync(int id);
    }
}
