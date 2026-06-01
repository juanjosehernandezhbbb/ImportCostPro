using ImportCostPro.Application.DTOs.Pais;
using ImportCostPro.Application.DTOs.Pais.ImportCostPro.Application.DTOs.Pais;
using ImportCostPro.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistance.Entities;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportCostPro.Application.Services
{
    public class PaisService : IPaisService
    {
        private readonly ImportCostProContext _context;

        public PaisService(ImportCostProContext context)
        {
            _context = context;
        }

        public async Task<int> CrearAsync(CrearPaisDto dto)
        {
            var existe = await _context.Set<Pais>().AnyAsync(p => p.CodigoISO.ToLower() == dto.CodigoISO.ToLower());
            if (existe) throw new Exception("Ya existe un país con este código ISO.");

            var nuevoPais = new Pais
            {
                Id = 0,
                Estado = true,
                Nombre = dto.Nombre,
                CodigoISO = dto.CodigoISO.ToUpper()
            };

            _context.Set<Pais>().Add(nuevoPais);
            await _context.SaveChangesAsync();
            return nuevoPais.Id;
        }

        public async Task<IEnumerable<PaisDto>> ObtenerTodosAsync()
        {
            return await _context.Set<Pais>()
                .Select(p => new PaisDto
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    CodigoISO = p.CodigoISO,
                    Estado = p.Estado
                }).ToListAsync();
        }

        public async Task<PaisDto> ObtenerPorIdAsync(int id)
        {
            var p = await _context.Set<Pais>().FindAsync(id);
            if (p == null) throw new Exception("País no encontrado.");

            return new PaisDto
            {
                Id = p.Id,
                Nombre = p.Nombre,
                CodigoISO = p.CodigoISO,
                Estado = p.Estado
            };
        }

        public async Task ActualizarAsync(int id, CrearPaisDto dto)
        {
            var p = await _context.Set<Pais>().FindAsync(id);
            if (p == null) throw new Exception("País no encontrado.");

            var existeIso = await _context.Set<Pais>().AnyAsync(x => x.CodigoISO == dto.CodigoISO && x.Id != id);
            if (existeIso) throw new Exception("El código ISO ya pertenece a otro país.");

            p.Nombre = dto.Nombre;
            p.CodigoISO = dto.CodigoISO.ToUpper();

            _context.Set<Pais>().Update(p);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int id)
        {
            var p = await _context.Set<Pais>().FindAsync(id);
            if (p == null) throw new Exception("País no encontrado.");

            _context.Set<Pais>().Remove(p);
            await _context.SaveChangesAsync();
        }
    }
}
