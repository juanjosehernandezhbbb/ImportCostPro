using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ImportCostPro.Application.DTOs.Importadores;
using ImportCostPro.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using Persistance.Entities;

namespace ImportCostPro.Application.Services
{
    public class ImportadorService : IImportadorService
    {
        private readonly ImportCostProContext _context;

        public ImportadorService(ImportCostProContext context)
        {
            _context = context;
        }

        public async Task<int> CrearAsync(CrearImportadorDto dto)
        {
            var existeRnc = await _context.Set<Importador>().AnyAsync(i => i.RNC == dto.RNC);
            if (existeRnc) throw new Exception("Ya existe un importador con este RNC.");

            var nuevo = new Importador
            {
                Id = 0,
                Estado = true,
                NombreRazonSocial = dto.NombreRazonSocial,
                RNC = dto.RNC,
                Telefono = dto.Telefono,
                Correo = dto.Correo,
                Direccion = dto.Direccion,
                PaisID = dto.PaisID
            };

            _context.Set<Importador>().Add(nuevo);
            await _context.SaveChangesAsync();
            return nuevo.Id;
        }

        public async Task<IEnumerable<ImportadorDto>> ObtenerTodosAsync()
        {
            return await _context.Set<Importador>()
                .Select(i => new ImportadorDto
                {
                    Id = i.Id,
                    NombreRazonSocial = i.NombreRazonSocial,
                    RNC = i.RNC,
                    Telefono = i.Telefono,
                    Correo = i.Correo,
                    Direccion = i.Direccion,
                    Estado = i.Estado,
                    PaisID = i.PaisID
                }).ToListAsync();
        }

        public async Task<ImportadorDto> ObtenerPorIdAsync(int id)
        {
            var i = await _context.Set<Importador>().FindAsync(id);
            if (i == null) throw new Exception("Importador no encontrado.");

            return new ImportadorDto
            {
                Id = i.Id,
                NombreRazonSocial = i.NombreRazonSocial,
                RNC = i.RNC,
                Telefono = i.Telefono,
                Correo = i.Correo,
                Direccion = i.Direccion,
                Estado = i.Estado,
                PaisID = i.PaisID
            };
        }

        public async Task ActualizarAsync(int id, CrearImportadorDto dto)
        {
            var i = await _context.Set<Importador>().FindAsync(id);
            if (i == null) throw new Exception("Importador no encontrado.");

            var existeRnc = await _context.Set<Importador>().AnyAsync(x => x.RNC == dto.RNC && x.Id != id);
            if (existeRnc) throw new Exception("El RNC ya pertenece a otro importador.");

            i.NombreRazonSocial = dto.NombreRazonSocial;
            i.RNC = dto.RNC;
            i.Telefono = dto.Telefono;
            i.Correo = dto.Correo;
            i.Direccion = dto.Direccion;
            i.PaisID = dto.PaisID;

            _context.Set<Importador>().Update(i);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int id)
        {
            var i = await _context.Set<Importador>().FindAsync(id);
            if (i == null) throw new Exception("Importador no encontrado.");

            _context.Set<Importador>().Remove(i);
            await _context.SaveChangesAsync();
        }
    }
}
