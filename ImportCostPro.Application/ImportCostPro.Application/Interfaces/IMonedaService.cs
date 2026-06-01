using ImportCostPro.Application.DTOs.Moneda;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ImportCostPro.Application.Interfaces
{
    public interface IMonedaService
    {
        Task<IEnumerable<MonedaDto>> ObtenerTodasAsync();
        Task<IEnumerable<MonedaDto>> ObtenerActivasAsync();
        Task<MonedaDto> ObtenerPorIdAsync(int id);

        Task<int> CrearAsync(CrearMonedaDTO dto);

        // Asumiendo que usarás el mismo MonedaDto para actualizar, o puedes crear un MonedaUpdateDto luego
        Task ActualizarAsync(MonedaDto dto);
        Task EliminarAsync(int id);

        Task<MonedaDto> ObtenerMonedaLocalAsync();
    }
}
