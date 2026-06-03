using ImportCostPro.Application.DTOs.Ordenes;
using ImportCostPro.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using Persistance.Entities;
using Persistance.Enums;

namespace ImportCostPro.Application.Services
{
    public class OrdenImportacionService : IOrdenImportacionService
    {
        private readonly ImportCostProContext _context;

        public OrdenImportacionService(ImportCostProContext context)
        {
            _context = context;
        }

        public async Task<int> CrearAsync(CrearOrdenDto dto)
        {
            var existeOrden = await _context.OrdenesImportacion
                .AnyAsync(o => o.NumeroOrden.ToLower().Trim() == dto.NumeroOrden.ToLower().Trim());

            if (existeOrden) throw new Exception("Ya existe una orden registrada con este número.");

            var nuevaOrden = new OrdenImportacion
            {
                NumeroOrden = dto.NumeroOrden,
                PaisOrigenID = dto.PaisOrigenID,
                MonedaID = dto.MonedaID,
                ImportadorID = dto.ImportadorID,
                ProveedorID = dto.ProveedorID,
                FechaOrden = dto.FechaOrden,
                modalidadTransporte = dto.ModalidadTransporte,
                estado = EstadoOrden.Abierta // Valor inicial por defecto
            };

            _context.OrdenesImportacion.Add(nuevaOrden);
            await _context.SaveChangesAsync();
            return nuevaOrden.Id;
        }

        public async Task<IEnumerable<OrdenImportacionDto>> ObtenerTodasAsync()
        {
            return await _context.OrdenesImportacion
                .Include(o => o.PaisOrigen).Include(o => o.Moneda)
                .Include(o => o.Importador).Include(o => o.Proveedor)
                .Select(o => new OrdenImportacionDto
                {
                    Id = o.Id,
                    NumeroOrden = o.NumeroOrden,
                    PaisOrigenID = o.PaisOrigenID,
                    NombrePaisOrigen = o.PaisOrigen!.Nombre,
                    MonedaID = o.MonedaID,
                    NombreMoneda = o.Moneda!.Nombre,
                    ImportadorID = o.ImportadorID,
                    NombreImportador = o.Importador!.NombreRazonSocial,
                    ProveedorID = o.ProveedorID,
                    NombreProveedor = o.Proveedor!.Nombre,
                    FechaOrden = o.FechaOrden,
                    ModalidadTransporte = o.modalidadTransporte,
                    Estado = o.estado
                })
                .OrderByDescending(o => o.FechaOrden).ToListAsync();
        }

        public async Task<OrdenImportacionDto> ObtenerPorIdAsync(int id)
        {
            var o = await _context.OrdenesImportacion
                .Include(ord => ord.PaisOrigen).Include(ord => ord.Moneda)
                .Include(ord => ord.Importador).Include(ord => ord.Proveedor)
                .FirstOrDefaultAsync(ord => ord.Id == id);

            if (o == null) throw new Exception("Orden no encontrada.");

            return new OrdenImportacionDto
            {
                Id = o.Id,
                NumeroOrden = o.NumeroOrden,
                PaisOrigenID = o.PaisOrigenID,
                NombrePaisOrigen = o.PaisOrigen!.Nombre,
                MonedaID = o.MonedaID,
                NombreMoneda = o.Moneda!.Nombre,
                ImportadorID = o.ImportadorID,
                NombreImportador = o.Importador!.NombreRazonSocial,
                ProveedorID = o.ProveedorID,
                NombreProveedor = o.Proveedor!.Nombre,
                FechaOrden = o.FechaOrden,
                ModalidadTransporte = o.modalidadTransporte,
                Estado = o.estado
            };
        }

        public async Task ActualizarAsync(int id, CrearOrdenDto dto)
        {
            var orden = await _context.OrdenesImportacion.FirstOrDefaultAsync(o => o.Id == id);
            if (orden == null) throw new Exception("Orden no encontrada.");

            // Validaciones de integridad para evitar errores de Foreign Key
            if (!await _context.Paises.AnyAsync(p => p.Id == dto.PaisOrigenID)) throw new Exception("País no válido.");
            if (!await _context.Monedas.AnyAsync(m => m.Id == dto.MonedaID)) throw new Exception("Moneda no válida.");
            if (!await _context.Importadores.AnyAsync(i => i.Id == dto.ImportadorID)) throw new Exception("Importador no válido.");
            if (!await _context.Proveedores.AnyAsync(p => p.Id == dto.ProveedorID)) throw new Exception("Proveedor no válido.");

            orden.NumeroOrden = dto.NumeroOrden;
            orden.PaisOrigenID = dto.PaisOrigenID;
            orden.MonedaID = dto.MonedaID;
            orden.ImportadorID = dto.ImportadorID;
            orden.ProveedorID = dto.ProveedorID;
            orden.FechaOrden = dto.FechaOrden;
            orden.modalidadTransporte = dto.ModalidadTransporte;
            orden.estado = dto.EstadoOrden;

            await _context.SaveChangesAsync();
        }

        public async Task CambiarEstadoAsync(int id, EstadoOrden nuevoEstado)
        {
            var orden = await _context.OrdenesImportacion.FindAsync(id);
            if (orden == null) throw new Exception("Orden no encontrada.");

            orden.estado = nuevoEstado;
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int id)
        {
            var orden = await _context.OrdenesImportacion.FindAsync(id);
            if (orden == null) throw new Exception("Orden no encontrada.");

            _context.OrdenesImportacion.Remove(orden);
            await _context.SaveChangesAsync();
        }
    }
}