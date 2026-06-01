using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ImportCostPro.Application.DTOs.Ordenes;
using ImportCostPro.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using Persistance.Entities;
using Persistance.Enums; // Para los Enums de tu compañero

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
            // Regla de Negocio 1: El número de orden no se puede repetir
            var existeOrden = await _context.OrdenesImportacion
                .AnyAsync(o => o.NumeroOrden.ToLower().Trim() == dto.NumeroOrden.ToLower().Trim());

            if (existeOrden)
                throw new Exception("Ya existe una orden de importación registrada con este número.");

            var nuevaOrden = new OrdenImportacion
            {
                Id = 0,
                NumeroOrden = dto.NumeroOrden,
                PaisOrigenID = dto.PaisOrigenID,
                MonedaID = dto.MonedaID,
                ImportadorID = dto.ImportadorID,
                ProveedorID = dto.ProveedorID,
                FechaOrden = dto.FechaOrden,
                Estado = true,

                // Nota: Usando minúsculas aquí porque así lo definió tu compañero en la entidad original
                modalidadTransporte = dto.ModalidadTransporte,

                // REGLA DE NEGOCIO ESTRICTA: Toda orden nueva nace en estado "Abierta"
                estado = EstadoOrden.Abierta
            };

            _context.OrdenesImportacion.Add(nuevaOrden);
            await _context.SaveChangesAsync();

            return nuevaOrden.Id;
        }

        public async Task<IEnumerable<OrdenImportacionDto>> ObtenerTodasAsync()
        {
            // Usamos .Include() para traer los nombres reales del país, moneda, etc., 
            // en lugar de mandar solo los IDs a la vista.
            return await _context.OrdenesImportacion
                .Include(o => o.PaisOrigen)
                .Include(o => o.Moneda)
                .Include(o => o.Importador)
                .Include(o => o.Proveedor)
                .Select(o => new OrdenImportacionDto
                {
                    Id = o.Id,
                    NumeroOrden = o.NumeroOrden,
                    PaisOrigenID = o.PaisOrigenID,
                    NombrePaisOrigen = o.PaisOrigen!.Nombre,
                    MonedaID = o.MonedaID,
                    NombreMoneda = o.Moneda!.Nombre,
                    ImportadorID = o.ImportadorID,
                    NombreImportador = o.Importador!.NombreRazonSocial, // o RazonSocial, según lo que tenga
                    ProveedorID = o.ProveedorID,
                    NombreProveedor = o.Proveedor!.Nombre,   // o RazonSocial
                    FechaOrden = o.FechaOrden,
                    ModalidadTransporte = o.modalidadTransporte,
                    Estado = o.estado
                })
                .OrderByDescending(o => o.FechaOrden) // Mostrar las más recientes primero
                .ToListAsync();
        }

        public async Task<OrdenImportacionDto> ObtenerPorIdAsync(int id)
        {
            var o = await _context.OrdenesImportacion
                .Include(ord => ord.PaisOrigen)
                .Include(ord => ord.Moneda)
                .Include(ord => ord.Importador)
                .Include(ord => ord.Proveedor)
                .FirstOrDefaultAsync(ord => ord.Id == id);

            if (o == null) throw new Exception("Orden de importación no encontrada.");

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
            var orden = await _context.OrdenesImportacion.FindAsync(id);
            if (orden == null) throw new Exception("Orden no encontrada.");

            // Regla de Negocio: Solo se pueden editar órdenes en estado "Abierta"
            if (orden.estado != EstadoOrden.Abierta)
            {
                throw new Exception("Solo se pueden modificar los datos generales de una orden si se encuentra en estado Abierta.");
            }

            // Validar que si cambian el número, no choque con otra orden existente
            var existeOrden = await _context.OrdenesImportacion
                .AnyAsync(o => o.NumeroOrden == dto.NumeroOrden && o.Id != id);
            if (existeOrden) throw new Exception("El nuevo número de orden ya está siendo utilizado.");

            orden.NumeroOrden = dto.NumeroOrden;
            orden.PaisOrigenID = dto.PaisOrigenID;
            orden.MonedaID = dto.MonedaID;
            orden.ImportadorID = dto.ImportadorID;
            orden.ProveedorID = dto.ProveedorID;
            orden.FechaOrden = dto.FechaOrden;
            orden.modalidadTransporte = dto.ModalidadTransporte;

            _context.OrdenesImportacion.Update(orden);
            await _context.SaveChangesAsync();
        }

        public async Task CambiarEstadoAsync(int id, EstadoOrden nuevoEstado)
        {
            var orden = await _context.OrdenesImportacion.FindAsync(id);
            if (orden == null) throw new Exception("Orden no encontrada.");

            // Regla de Negocio: No se puede reabrir una orden cerrada
            if (orden.estado == EstadoOrden.Cerrada && nuevoEstado != EstadoOrden.Cerrada)
            {
                throw new Exception("Una orden Cerrada ya contiene cálculos oficiales inmutables y no puede cambiar de estado.");
            }

            // Si pasa las validaciones, actualizamos el estado
            orden.estado = nuevoEstado;
            _context.OrdenesImportacion.Update(orden);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int id)
        {
            var orden = await _context.OrdenesImportacion.FindAsync(id);
            if (orden == null) throw new Exception("Orden no encontrada.");

            // Regla de Negocio: No borrar órdenes calculadas o cerradas por integridad fiscal
            if (orden.estado == EstadoOrden.Calculada || orden.estado == EstadoOrden.Cerrada)
            {
                throw new Exception("No es posible eliminar una orden que ya tiene un cálculo de Landed Cost en progreso o definitivo. Debe cancelarla.");
            }

            _context.OrdenesImportacion.Remove(orden);
            await _context.SaveChangesAsync();
        }
    }
}
