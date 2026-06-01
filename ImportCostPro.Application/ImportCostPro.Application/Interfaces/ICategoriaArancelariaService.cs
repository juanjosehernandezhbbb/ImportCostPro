using ImportCostPro.Application.DTOs.CategoriasArancelarias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportCostPro.Application.Interfaces
{
    public interface ICategoriaArancelariaService
    {
        Task<IEnumerable<CategoriaArancelariaDto>> ObtenerTodasAsync();
        Task<CategoriaArancelariaDto> ObtenerPorIdAsync(int id);
        Task<int> CrearAsync(CategoriaArancelariaCreateDto dto);
        Task ActualizarAsync(int id, CategoriaArancelariaCreateDto dto);
        Task EliminarAsync(int id);
    }
}
