using ImportCostPro.Application.DTOs.DetallesOrden;
using ImportCostPro.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using Persistance.Entities;
using Persistance.Enums;

namespace ImportCostPro.Application.Services
{
    public class DetalleOrdenService : IDetalleOrdenService
    {
        private readonly ImportCostProContext _context;

        public DetalleOrdenService(ImportCostProContext context)
        {
            _context = context;
        }

        public async Task<int> AgregarProductoAsync(AgregarProductoOrdenDto dto)
        {
            var orden = await _context.OrdenesImportacion.FindAsync(dto.OrdenImportacionId);
            if (orden == null) throw new Exception("La orden de importación no existe.");

            if (orden.estado != EstadoOrden.Abierta)
            {
                throw new Exception($"No se pueden agregar productos a una orden que está en estado {orden.estado}.");
            }

            var producto = await _context.Productos.FindAsync(dto.ProductoId);
            if (producto == null) throw new Exception("El producto seleccionado no existe.");

            if (dto.MargenDeseado < 0 || dto.MargenDeseado >= 100)
            {
                throw new Exception("El margen de ganancia debe estar entre 0% y 99%.");
            }

            // Lógica de Upsert: Evita el error de clave duplicada sumando cantidades
            var detalleExistente = await _context.DetallesOrden
                .FirstOrDefaultAsync(d => d.OrdenImportacionId == dto.OrdenImportacionId
                                       && d.ProductoId == dto.ProductoId);

            if (detalleExistente != null)
            {
                detalleExistente.Cantidad += dto.Cantidad;
                detalleExistente.PrecioUnitarioFOB = dto.PrecioUnitarioFOB;
                detalleExistente.MargenDeseado = dto.MargenDeseado;
                detalleExistente.Descripcion = dto.Descripcion;

                await _context.SaveChangesAsync();
                return detalleExistente.Id;
            }

            // Crear nuevo registro si no existe
            var nuevoDetalle = new DetalleOrden
            {
                OrdenImportacionId = dto.OrdenImportacionId,
                ProductoId = dto.ProductoId,
                Descripcion = dto.Descripcion,
                Cantidad = dto.Cantidad,
                PrecioUnitarioFOB = dto.PrecioUnitarioFOB,
                MargenDeseado = dto.MargenDeseado
            };

            _context.DetallesOrden.Add(nuevoDetalle);
            await _context.SaveChangesAsync();

            return nuevoDetalle.Id;
        }

        public async Task<IEnumerable<DetalleOrdenDto>> ObtenerPorOrdenIdAsync(int ordenId)
        {
            return await _context.DetallesOrden
                .Include(d => d.Producto)
                .Where(d => d.OrdenImportacionId == ordenId)
                .Select(d => new DetalleOrdenDto
                {
                    Id = d.Id,
                    OrdenImportacionId = d.OrdenImportacionId,
                    ProductoId = d.ProductoId,
                    NombreProducto = d.Producto!.Nombre,
                    Descripcion = d.Descripcion,
                    Cantidad = d.Cantidad,
                    PrecioUnitarioFOB = d.PrecioUnitarioFOB,
                    MargenDeseado = d.MargenDeseado
                })
                .ToListAsync();
        }

        public async Task EliminarProductoAsync(int detalleId)
        {
            var detalle = await _context.DetallesOrden
                .Include(d => d.OrdenImportacion)
                .FirstOrDefaultAsync(d => d.Id == detalleId);

            if (detalle == null) throw new Exception("El producto no fue encontrado.");

            if (detalle.OrdenImportacion.estado != EstadoOrden.Abierta)
            {
                throw new Exception("No se pueden eliminar productos de una orden cerrada.");
            }

            _context.DetallesOrden.Remove(detalle);
            await _context.SaveChangesAsync();
        }
    }
}