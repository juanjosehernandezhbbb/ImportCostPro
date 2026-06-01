using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public TasaCambioService(ImportCostProContext context)
        {
            _context = context;
        }

        public async Task<int> CrearAsync(CrearTasaCambioDto dto)
        {
            // Reglas de negocio básicas
            if (dto.MonedaOrigenID == dto.MonedaDestinoID)
                throw new Exception("La moneda de origen y destino no pueden ser la misma.");

            if (dto.ValorTasa <= 0)
                throw new Exception("El valor de la tasa debe ser mayor a cero.");

            // Evitar que registren la misma tasa para las mismas monedas el mismo día
            var existeTasaDia = await _context.TasaCambios.AnyAsync(t =>
                t.MonedaOrigenID == dto.MonedaOrigenID &&
                t.MonedaDestinoID == dto.MonedaDestinoID &&
                t.FechaVigencia.Date == dto.FechaVigencia.Date);

            if (existeTasaDia)
                throw new Exception("Ya existe una tasa de cambio registrada para estas monedas en esta fecha.");

            var nuevaTasa = new TasaCambio
            {
                Id = 0,
                Estado = true,
                MonedaOrigenID = dto.MonedaOrigenID,
                MonedaDestinoID = dto.MonedaDestinoID,
                ValorTasa = dto.ValorTasa,
                FechaVigencia = dto.FechaVigencia
            };

            _context.TasaCambios.Add(nuevaTasa);
            await _context.SaveChangesAsync();
            return nuevaTasa.Id;
        }

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
                    FechaVigencia = t.FechaVigencia,
                    Estado = t.Estado
                })
                .OrderByDescending(t => t.FechaVigencia) // Mostrar las más recientes arriba
                .ToListAsync();
        }

        public async Task EliminarAsync(int id)
        {
            var tasa = await _context.TasaCambios.FindAsync(id);
            if (tasa == null) throw new Exception("Tasa de cambio no encontrada.");

            _context.TasaCambios.Remove(tasa);
            await _context.SaveChangesAsync();
        }
    }
}
