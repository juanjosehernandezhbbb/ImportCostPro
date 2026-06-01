using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImportCostPro.Application.DTOs.CategoriasArancelarias;
using ImportCostPro.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using Persistance.Entities;



namespace ImportCostPro.Application.Services
{
    public class CategoriaArancelariaService : ICategoriaArancelariaService
    {
        private readonly ImportCostProContext _context;

        public CategoriaArancelariaService(ImportCostProContext context)
        {
            _context = context;
        }

        public async Task<int> CrearAsync(CategoriaArancelariaCreateDto dto)
        {
            // Usamos CodigoArancelio porque así está en la entidad
            var existe = await _context.Set<CategoriaArancelaria>().AnyAsync(c => c.CodigoArancelio.ToLower() == dto.Codigo.ToLower());
            if (existe) throw new Exception("Ya existe una categoría arancelaria con este código.");

            var nueva = new CategoriaArancelaria
            {
                Id = 0,
                Estado = dto.Activo,
                CodigoArancelio = dto.Codigo, 
                Nombre = dto.Nombre,
                PorcentajeArancel = dto.PorcentajeArancel,
                AplicaITBS = dto.AplicaITBIS, 
                AplicaSelectivo = dto.AplicaImpuestoSelectivo, 
                PorctImptSelectivo = dto.PorcentajeImpuestoSelectivo 
            };

            _context.Set<CategoriaArancelaria>().Add(nueva);
            await _context.SaveChangesAsync();
            return nueva.Id;
        }

        public async Task<IEnumerable<CategoriaArancelariaDto>> ObtenerTodasAsync()
        {
            return await _context.Set<CategoriaArancelaria>()
                .Select(c => new CategoriaArancelariaDto
                {
                    Id = c.Id,
                    Codigo = c.CodigoArancelio, 
                    Nombre = c.Nombre,
                    PorcentajeArancel = c.PorcentajeArancel,
                    AplicaITBIS = c.AplicaITBS,
                    AplicaImpuestoSelectivo = c.AplicaSelectivo,
                    PorcentajeImpuestoSelectivo = c.PorctImptSelectivo ?? 0m, // Si es null, mandamos 0
                    Activo = c.Estado
                }).ToListAsync();
        }

        public async Task<CategoriaArancelariaDto> ObtenerPorIdAsync(int id)
        {
            var c = await _context.Set<CategoriaArancelaria>().FindAsync(id);
            if (c == null) throw new Exception("Categoría arancelaria no encontrada.");

            return new CategoriaArancelariaDto
            {
                Id = c.Id,
                Codigo = c.CodigoArancelio,
                Nombre = c.Nombre,
                PorcentajeArancel = c.PorcentajeArancel,
                AplicaITBIS = c.AplicaITBS,
                AplicaImpuestoSelectivo = c.AplicaSelectivo,
                PorcentajeImpuestoSelectivo = c.PorctImptSelectivo ?? 0m,
                Activo = c.Estado
            };
        }

        public async Task ActualizarAsync(int id, CategoriaArancelariaCreateDto dto)
        {
            var c = await _context.Set<CategoriaArancelaria>().FindAsync(id);
            if (c == null) throw new Exception("Categoría arancelaria no encontrada.");

            var existe = await _context.Set<CategoriaArancelaria>().AnyAsync(x => x.CodigoArancelio.ToLower() == dto.Codigo.ToLower() && x.Id != id);
            if (existe) throw new Exception("El código ya pertenece a otra categoría.");

            c.Estado = dto.Activo;
            c.CodigoArancelio = dto.Codigo;
            c.Nombre = dto.Nombre;
            c.PorcentajeArancel = dto.PorcentajeArancel;
            c.AplicaITBS = dto.AplicaITBIS;
            c.AplicaSelectivo = dto.AplicaImpuestoSelectivo;
            c.PorctImptSelectivo = dto.PorcentajeImpuestoSelectivo;

            _context.Set<CategoriaArancelaria>().Update(c);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int id)
        {
            var c = await _context.Set<CategoriaArancelaria>().FindAsync(id);
            if (c == null) throw new Exception("Categoría arancelaria no encontrada.");

            _context.Set<CategoriaArancelaria>().Remove(c);
            await _context.SaveChangesAsync();
        }
    }
}

