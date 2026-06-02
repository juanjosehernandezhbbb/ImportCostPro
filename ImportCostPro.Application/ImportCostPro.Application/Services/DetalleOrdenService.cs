using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ImportCostPro.Application.DTOs.DetallesOrden;
using ImportCostPro.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using Persistance.Entities;
using Persistance.Enums;
using ImportCostPro.Application.DTOs.Producto;

namespace ImportCostPro.Application.Services
{
    public class DetalleOrdenService : IDetalleOrdenService
    {
        private readonly ImportCostProContext _context;

        public DetalleOrdenService(ImportCostProContext context)
        {
            _context = context;
        }

        // CORRECCIÓN 1: Usamos la ruta completa del DTO para evitar la ambigüedad
        public async Task<int> AgregarProductoAsync(ImportCostPro.Application.DTOs.DetallesOrden.AgregarProductoOrdenDto dto)
        {
            // 1. Buscamos la orden para verificar su estado
            var orden = await _context.OrdenesImportacion.FindAsync(dto.OrdenImportacionId);
            if (orden == null) throw new Exception("La orden de importación no existe.");

            // REGLA DE NEGOCIO ESTRICTA: Solo se pueden agregar productos si la orden está "Abierta"
            if (orden.estado != EstadoOrden.Abierta)
            {
                throw new Exception($"No se pueden agregar productos a una orden que está en estado {orden.estado}.");
            }

            // 2. Validamos que el producto a agregar realmente exista y esté activo
            // CORRECCIÓN 2: Cambiamos _context.Producto a _context.Productos (en plural)
            var producto = await _context.Set<Producto>().FindAsync(dto.ProductoId);
            if (producto == null) throw new Exception("El producto seleccionado no existe.");

            // (Opcional) Validar que el margen de ganancia sea lógico (ej. mayor o igual a 0 y menor que 100)
            if (dto.MargenDeseado < 0 || dto.MargenDeseado >= 100)
            {
                throw new Exception("El margen de ganancia debe estar entre 0% y 99%.");
            }

            // 3. Creamos el registro
            var nuevoDetalle = new DetalleOrden
            {
                Id = 0,             // Para BasicEntity
                Estado = true,      // Para BasicEntity
                OrdenImportacionId = dto.OrdenImportacionId,
                ProductoId = dto.ProductoId,
                Descripcion = dto.Descripcion,
                Cantidad = dto.Cantidad,
                PrecioUnitarioFOB = dto.PrecioUnitarioFOB,
                MargenDeseado = dto.MargenDeseado
            };

            _context.DetallesOrdenImportacion.Add(nuevoDetalle);
            await _context.SaveChangesAsync();

            return nuevoDetalle.Id;
        }

        public async Task<IEnumerable<DetalleOrdenDto>> ObtenerPorOrdenIdAsync(int ordenId)
        {
            // Usamos Include para traer el nombre del producto y no mostrar solo un número en la vista
            return await _context.DetallesOrdenImportacion
                .Include(d => d.Producto)
                .Where(d => d.OrdenImportacionId == ordenId)
                .Select(d => new DetalleOrdenDto
                {
                    Id = d.Id,
                    OrdenImportacionId = d.OrdenImportacionId,
                    ProductoId = d.ProductoId,
                    NombreProducto = d.Producto!.Nombre, // Tu compañero le puso 'Nombre' al Producto
                    Descripcion = d.Descripcion,
                    Cantidad = d.Cantidad,
                    PrecioUnitarioFOB = d.PrecioUnitarioFOB,
                    MargenDeseado = d.MargenDeseado
                })
                .ToListAsync();
        }

        public async Task EliminarProductoAsync(int detalleId)
        {
            // Buscamos el detalle e incluimos la orden para ver su estado
            var detalle = await _context.DetallesOrdenImportacion
                .Include(d => d.OrdenImportacion)
                .FirstOrDefaultAsync(d => d.Id == detalleId);

            if (detalle == null) throw new Exception("El producto no fue encontrado en esta orden.");

            // REGLA DE NEGOCIO ESTRICTA: Validar estado de la orden antes de borrar
            if (detalle.OrdenImportacion.estado != EstadoOrden.Abierta)
            {
                throw new Exception($"No se pueden eliminar productos porque la orden ya está {detalle.OrdenImportacion.estado}.");
            }

            _context.DetallesOrdenImportacion.Remove(detalle);
            await _context.SaveChangesAsync();
        }
    }
}