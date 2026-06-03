using ImportCostPro.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using Persistance.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImportCostPro.Application.DTOs.Moneda;

namespace ImportCostPro.Application.Services
{
    public class MonedaService : IMonedaService
    {
        private readonly ImportCostProContext _context;

        public MonedaService(ImportCostProContext context)
        {
            _context = context;
        }

        public async Task<int> CrearAsync(CrearMonedaDTO dto)
        {
            dto.CodigoISO = dto.CodigoISO.ToUpper().Trim();

            var existeIso = await _context.Monedas.AnyAsync(m => m.CodigoIso == dto.CodigoISO);
            if (existeIso)
                throw new Exception("Ya existe una moneda registrada con este código ISO.");

            if (dto.EsMonedaLocal)
            {
                var existeMonedaLocal = await _context.Monedas.AnyAsync(m => m.EsMonedaLocal == true);
                if (existeMonedaLocal)
                    throw new Exception("Ya existe una moneda configurada como moneda local.");
            }

            var nuevaMoneda = new Moneda
            {
                Nombre = dto.Nombre,
                CodigoIso = dto.CodigoISO,
                Simbolo = dto.Simbolo,
                EsMonedaLocal = dto.EsMonedaLocal,
                Estado = true // Asumiendo propiedad de BasicEntity
            };

            _context.Monedas.Add(nuevaMoneda);
            await _context.SaveChangesAsync();

            return nuevaMoneda.Id;
        }

        public async Task<IEnumerable<MonedaDto>> ObtenerTodasAsync()
        {
            return await _context.Monedas
                .Select(m => new MonedaDto
                {
                    Id = m.Id,
                    Nombre = m.Nombre,
                    CodigoISO = m.CodigoIso,
                    Simbolo = m.Simbolo,
                    EsMonedaLocal = m.EsMonedaLocal ?? false,
                    Activo = m.Estado // Ajusta 'Estado' si tu entidad usa otro nombre
                }).ToListAsync();
        }

        public async Task<IEnumerable<MonedaDto>> ObtenerActivasAsync()
        {
            return await _context.Monedas
                .Where(m => m.Estado) // Filtro por activos
                .Select(m => new MonedaDto
                {
                    Id = m.Id,
                    Nombre = m.Nombre,
                    CodigoISO = m.CodigoIso,
                    Simbolo = m.Simbolo,
                    EsMonedaLocal = m.EsMonedaLocal ?? false,
                    Activo = m.Estado
                }).ToListAsync();
        }

        public async Task<MonedaDto> ObtenerPorIdAsync(int id)
        {
            var moneda = await _context.Monedas.FindAsync(id);
            if (moneda == null) throw new Exception("Moneda no encontrada.");

            return new MonedaDto
            {
                Id = moneda.Id,
                Nombre = moneda.Nombre,
                CodigoISO = moneda.CodigoIso,
                Simbolo = moneda.Simbolo,
                EsMonedaLocal = moneda.EsMonedaLocal ?? false,
                Activo = moneda.Estado
            };
        }

        public async Task<MonedaDto> ObtenerMonedaLocalAsync()
        {
            var monedaLocal = await _context.Monedas.FirstOrDefaultAsync(m => m.EsMonedaLocal == true);
            if (monedaLocal == null) throw new Exception("No hay moneda local configurada.");

            return new MonedaDto
            {
                Id = monedaLocal.Id,
                Nombre = monedaLocal.Nombre,
                CodigoISO = monedaLocal.CodigoIso,
                Simbolo = monedaLocal.Simbolo,
                EsMonedaLocal = true,
                Activo = monedaLocal.Estado
            };
        }

        public async Task ActualizarAsync(MonedaDto dto)
        {
            var moneda = await _context.Monedas.FindAsync(dto.Id);
            if (moneda == null) throw new Exception("Moneda no encontrada.");

            dto.CodigoISO = dto.CodigoISO.ToUpper().Trim();

            var existeIso = await _context.Monedas.AnyAsync(m => m.CodigoIso == dto.CodigoISO && m.Id != dto.Id);
            if (existeIso) throw new Exception("Ya existe otra moneda con este código ISO.");

            if (dto.EsMonedaLocal && moneda.EsMonedaLocal != true)
            {
                var existeMonedaLocal = await _context.Monedas.AnyAsync(m => m.EsMonedaLocal == true && m.Id != dto.Id);
                if (existeMonedaLocal) throw new Exception("Ya existe una moneda local en el sistema.");
            }

            moneda.Nombre = dto.Nombre;
            moneda.CodigoIso = dto.CodigoISO;
            moneda.Simbolo = dto.Simbolo;
            moneda.EsMonedaLocal = dto.EsMonedaLocal;
            moneda.Estado = dto.Activo;

            _context.Monedas.Update(moneda);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int id)
        {
            var moneda = await _context.Monedas.FindAsync(id);
            if (moneda == null) throw new Exception("Moneda no encontrada.");

            if (moneda.EsMonedaLocal == true)
                throw new Exception("No se puede eliminar la moneda local del sistema.");

            _context.Monedas.Remove(moneda);
            await _context.SaveChangesAsync();
        }
    }
}