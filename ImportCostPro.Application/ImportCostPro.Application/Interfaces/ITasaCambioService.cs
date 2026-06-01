using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ImportCostPro.Application.DTOs.TasasCambio;

namespace ImportCostPro.Application.Interfaces
{
    public interface ITasaCambioService
    {
        Task<IEnumerable<TasaCambioDto>> ObtenerTodasAsync();
        Task<int> CrearAsync(CrearTasaCambioDto dto);
        Task EliminarAsync(int id);
    }
}