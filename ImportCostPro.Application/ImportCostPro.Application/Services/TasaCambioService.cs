using ImportCostPro.Application.DTOs.TasasCambio;
using ImportCostPro.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using Persistance.Entities;

namespace ImportCostPro.Application.Services
{
    public class TasaCambioService : ITasaCambioService
    {
        private readonly ImportCostProContext _context;

        public TasaCambioService(ImportCostProContext context) => _context = context;

        // ... [ObtenerTodasAsync, ObtenerPorIdAsync, ActualizarAsync y EliminarAsync permanecen igual] ...

        public async Task<int> CrearAsync(CrearTasaCambioDto dto) // Cambiado a Task<int>
        {
            if (dto.MonedaOrigenID == dto.MonedaDestinoID)
                throw new Exception("La moneda de origen y destino no pueden ser la misma.");

            var nuevaTasa = new TasaCambio
            {
                MonedaOrigenID = dto.MonedaOrigenID,
                MonedaDestinoID = dto.MonedaDestinoID,
                ValorTasa = dto.ValorTasa,
                FechaVigencia = dto.FechaVigencia
            };

            _context.TasaCambios.Add(nuevaTasa);
            await _context.SaveChangesAsync();

            return nuevaTasa.Id; // Ahora retorna el ID generado, coincidiendo con la interfaz
        }

        // --- Asegúrate de que el resto del archivo se vea así: ---

        public async Task<IEnumerable<TasaCambioDto>> ObtenerTodasAsync()
        {
            return await _context.TasaCambios
                .Include(t => t.MonedaOrigen)
                .Include(t => t.MonedaDestino)
                .Select(t => new TasaCambioDto
                {
                    Id = t.Id,
                    MonedaOrigenID = t.MonedaOrigenID,
                    NombreMonedaOrigen = t.MonedaOrigen != null ? t.MonedaOrigen.Nombre : "N/A",
                    MonedaDestinoID = t.MonedaDestinoID,
                    NombreMonedaDestino = t.MonedaDestino != null ? t.MonedaDestino.Nombre : "N/A",
                    ValorTasa = t.ValorTasa,
                    FechaVigencia = t.FechaVigencia
                })
                .OrderByDescending(t => t.FechaVigencia)
                .ToListAsync();
        }

        public async Task<CrearTasaCambioDto> ObtenerPorIdAsync(int id)
        {
            var tasa = await _context.TasaCambios.FindAsync(id);
            if (tasa == null) throw new Exception("Tasa no encontrada.");
            return new CrearTasaCambioDto { MonedaOrigenID = tasa.MonedaOrigenID, MonedaDestinoID = tasa.MonedaDestinoID, ValorTasa = tasa.ValorTasa, FechaVigencia = tasa.FechaVigencia };
        }

        public async Task ActualizarAsync(int id, CrearTasaCambioDto dto)
        {
            var tasa = await _context.TasaCambios.FindAsync(id);
            if (tasa == null) throw new Exception("Tasa no encontrada.");
            tasa.MonedaOrigenID = dto.MonedaOrigenID;
            tasa.MonedaDestinoID = dto.MonedaDestinoID;
            tasa.ValorTasa = dto.ValorTasa;
            tasa.FechaVigencia = dto.FechaVigencia;
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int id)
        {
            var tasa = await _context.TasaCambios.FindAsync(id);
            if (tasa != null) { _context.TasaCambios.Remove(tasa); await _context.SaveChangesAsync(); }
        }
    }
}