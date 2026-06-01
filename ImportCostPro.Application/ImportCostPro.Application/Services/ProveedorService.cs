using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ImportCostPro.Application.DTOs.Proveedores;
using ImportCostPro.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using Persistance.Entities;

using ImportCostPro.Application.DTOs.Proveedores;
using ImportCostPro.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using Persistance.Entities;

namespace ImportCostPro.Application.Services
{
    public class ProveedorService : IProveedorService
    {
        private readonly ImportCostProContext _context;

        public ProveedorService(ImportCostProContext context)
        {
            _context = context;
        }

        public async Task<int> CrearAsync(CrearProveedorDto dto)
        {
            // Cambiamos la validación para que no se repita el nombre del proveedor
            var existe = await _context.Set<Proveedor>().AnyAsync(p => p.Nombre.ToLower() == dto.Nombre.ToLower());
            if (existe) throw new Exception("Ya existe un proveedor registrado con este nombre.");

            var nuevo = new Proveedor
            {
                Id = 0,
                Estado = true,
                Nombre = dto.Nombre,
                Telefono = dto.Telefono,
                Correo = dto.Correo,
                Direccion = dto.Direccion,
                PaisOrigenID = dto.PaisOrigenID, 
                MonedaPrincipalID = dto.MonedaPrincipalID
            };

            _context.Set<Proveedor>().Add(nuevo);
            await _context.SaveChangesAsync();
            return nuevo.Id;
        }

        public async Task<IEnumerable<ProveedorDto>> ObtenerTodosAsync()
        {
            return await _context.Set<Proveedor>()
                .Select(p => new ProveedorDto
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    Telefono = p.Telefono,
                    Correo = p.Correo,
                    Direccion = p.Direccion,
                    Estado = p.Estado,
                    PaisOrigenID = p.PaisOrigenID,
                    MonedaPrincipalID = p.MonedaPrincipalID
                }).ToListAsync();
        }

        public async Task<ProveedorDto> ObtenerPorIdAsync(int id)
        {
            var p = await _context.Set<Proveedor>().FindAsync(id);
            if (p == null) throw new Exception("Proveedor no encontrado.");

            return new ProveedorDto
            {
                Id = p.Id,
                Nombre = p.Nombre,
                Telefono = p.Telefono,
                Correo = p.Correo,
                Direccion = p.Direccion,
                Estado = p.Estado,
                PaisOrigenID = p.PaisOrigenID,
                MonedaPrincipalID = p.MonedaPrincipalID
            };
        }

        public async Task ActualizarAsync(int id, CrearProveedorDto dto)
        {
            var p = await _context.Set<Proveedor>().FindAsync(id);
            if (p == null) throw new Exception("Proveedor no encontrado.");

            var existe = await _context.Set<Proveedor>().AnyAsync(x => x.Nombre.ToLower() == dto.Nombre.ToLower() && x.Id != id);
            if (existe) throw new Exception("El nombre ya pertenece a otro proveedor registrado.");

            p.Nombre = dto.Nombre;
            p.Telefono = dto.Telefono;
            p.Correo = dto.Correo;
            p.Direccion = dto.Direccion;
            p.PaisOrigenID = dto.PaisOrigenID;
            p.MonedaPrincipalID = dto.MonedaPrincipalID;

            _context.Set<Proveedor>().Update(p);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int id)
        {
            var p = await _context.Set<Proveedor>().FindAsync(id);
            if (p == null) throw new Exception("Proveedor no encontrado.");

            _context.Set<Proveedor>().Remove(p);
            await _context.SaveChangesAsync();
        }
    }
}

