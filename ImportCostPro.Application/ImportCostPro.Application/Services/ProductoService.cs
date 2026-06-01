using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImportCostPro.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using Persistance.Entities;
using ImportCostPro.Application.DTOs.Producto;

namespace ImportCostPro.Application.Services
{
    public class ProductoService : IProductoService
    {
        private readonly ImportCostProContext _context;

        public ProductoService(ImportCostProContext context)
        {
            _context = context;
        }

        public async Task<int> CrearAsync(CrearProductoDto dto)
        {
            // Regla de Negocio: El código de producto debe ser único
            var existeCodigo = await _context.Set<Producto>().AnyAsync(p => p.Codigo.Trim().ToLower() == dto.Codigo.Trim().ToLower());
            if (existeCodigo) throw new Exception("Ya existe un producto registrado con este código.");

            var nuevoProducto = new Producto
            {
                Id = 0,
                Estado = true,
                Nombre = dto.Nombre,
                Codigo = dto.Codigo,
                PaisOrigenID = dto.PaisOrigenID,
                CategoriaArancelariaID = dto.CategoriaArancelariaID,
                PesoUnitario = dto.PesoUnitario,
                Largo = dto.Largo,
                Ancho = dto.Ancho,
                Alto = dto.Alto,
                Descripcion = dto.Descripcion,
                UnidadDeMedidaID = dto.UnidadDeMedidaID
            };

            _context.Set<Producto>().Add(nuevoProducto);
            await _context.SaveChangesAsync();
            return nuevoProducto.Id;
        }

        public async Task<IEnumerable<ProductoDto>> ObtenerTodosAsync()
        {
            return await _context.Set<Producto>()
                .Include(p => p.PaisOrigen)
                .Include(p => p.CategoriaArancelaria)
                .Select(p => new ProductoDto
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    Codigo = p.Codigo,
                    PaisOrigenID = p.PaisOrigenID,
                    NombrePaisOrigen = p.PaisOrigen != null ? p.PaisOrigen.Nombre : "N/A",
                    CategoriaArancelariaID = p.CategoriaArancelariaID,
                    DescripcionCategoria = p.CategoriaArancelaria != null ? "Categoría asignada" : "N/A", // Ajusta según las propiedades reales de tu CategoriaArancelaria
                    PesoUnitario = p.PesoUnitario,
                    Largo = p.Largo,
                    Ancho = p.Ancho,
                    Alto = p.Alto,
                    Descripcion = p.Descripcion,
                    UnidadDeMedidaID = p.UnidadDeMedidaID,
                    Estado = p.Estado
                }).ToListAsync();
        }

        public async Task<ProductoDto> ObtenerPorIdAsync(int id)
        {
            var p = await _context.Set<Producto>()
                .Include(prod => prod.PaisOrigen)
                .Include(prod => prod.CategoriaArancelaria)
                .FirstOrDefaultAsync(prod => prod.Id == id);

            if (p == null) throw new Exception("Producto no encontrado.");

            return new ProductoDto
            {
                Id = p.Id,
                Nombre = p.Nombre,
                Codigo = p.Codigo,
                PaisOrigenID = p.PaisOrigenID,
                NombrePaisOrigen = p.PaisOrigen != null ? p.PaisOrigen.Nombre : "N/A",
                CategoriaArancelariaID = p.CategoriaArancelariaID,
                DescripcionCategoria = p.CategoriaArancelaria != null ? "Categoría asignada" : "N/A",
                PesoUnitario = p.PesoUnitario,
                Largo = p.Largo,
                Ancho = p.Ancho,
                Alto = p.Alto,
                Descripcion = p.Descripcion,
                UnidadDeMedidaID = p.UnidadDeMedidaID,
                Estado = p.Estado
            };
        }

        public async Task ActualizarAsync(int id, CrearProductoDto dto)
        {
            var p = await _context.Set<Producto>().FindAsync(id);
            if (p == null) throw new Exception("Producto no encontrado.");

            var existeCodigo = await _context.Set<Producto>().AnyAsync(x => x.Codigo == dto.Codigo && x.Id != id);
            if (existeCodigo) throw new Exception("El código ya está siendo utilizado por otro producto.");

            p.Nombre = dto.Nombre;
            p.Codigo = dto.Codigo;
            p.PaisOrigenID = dto.PaisOrigenID;
            p.CategoriaArancelariaID = dto.CategoriaArancelariaID;
            p.PesoUnitario = dto.PesoUnitario;
            p.Largo = dto.Largo;
            p.Ancho = dto.Ancho;
            p.Alto = dto.Alto;
            p.Descripcion = dto.Descripcion;
            p.UnidadDeMedidaID = dto.UnidadDeMedidaID;

            _context.Set<Producto>().Update(p);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int id)
        {
            var p = await _context.Set<Producto>().FindAsync(id);
            if (p == null) throw new Exception("Producto no encontrado.");
            _context.Set<Producto>().Remove(p);
            await _context.SaveChangesAsync();
        }
    }
}
