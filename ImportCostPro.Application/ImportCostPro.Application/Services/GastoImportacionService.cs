using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImportCostPro.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using Persistance.Entities;
using Persistance.Enums;
using ImportCostPro.Application.DTOs.Gasto;

namespace ImportCostPro.Application.Services
{
    public class GastoImportacionService : IGastoImportacionService
    {
        private readonly ImportCostProContext _context;

        public GastoImportacionService(ImportCostProContext context)
        {
            _context = context;
        }

        public async Task<int> AgregarGastoAsync(AgregarGastoDto dto)
        {
            // 1. Buscamos la orden
            var orden = await _context.OrdenesImportacion.FindAsync(dto.OrdenImportacionID);
            if (orden == null) throw new Exception("La orden de importación no existe.");

            // 2. REGLA DE NEGOCIO ESTRICTA: Validar estado de la orden
            if (orden.estado != EstadoOrden.Abierta)
            {
                throw new Exception($"No se pueden registrar gastos en una orden que está en estado {orden.estado}.");
            }

            // 3. Crear el gasto
            var nuevoGasto = new GastoImportacion
            {
                Id = 0,             // Para calmar al BasicEntity
                Estado = true,      // Para calmar al BasicEntity
                OrdenImportacionID = dto.OrdenImportacionID,
                MonedaID = dto.MonedaID,
                Monto = dto.Monto,
                FechaGasto = dto.FechaGasto,

                // TipoGasto = dto.TipoGasto, // <- Descomentar si Persona 1 ya lo agregó a la entidad
                MetodoDistribucion = dto.MetodoDistribucion
            };

            _context.GastosImportacion.Add(nuevoGasto);
            await _context.SaveChangesAsync();

            return nuevoGasto.Id;
        }

        public async Task<IEnumerable<GastoImportacionDto>> ObtenerPorOrdenIdAsync(int ordenId)
        {
            return await _context.GastosImportacion
                .Include(g => g.Moneda)
                .Where(g => g.OrdenImportacionID == ordenId)
                .Select(g => new GastoImportacionDto
                {
                    Id = g.Id,
                    OrdenImportacionID = g.OrdenImportacionID,
                    MonedaID = g.MonedaID,
                    NombreMoneda = g.Moneda!.Nombre,
                    SimboloMoneda = g.Moneda!.Simbolo,
                    Monto = g.Monto,
                    FechaGasto = g.FechaGasto,
                    // TipoGasto = g.TipoGasto, // <- Descomentar si Persona 1 ya lo agregó a la entidad
                    MetodoDistribucion = g.MetodoDistribucion
                })
                .ToListAsync();
        }

        public async Task ActualizarGastoAsync(int id, AgregarGastoDto dto)
        {
            var gasto = await _context.GastosImportacion
                .Include(g => g.OrdenImportacion)
                .FirstOrDefaultAsync(g => g.Id == id);

            if (gasto == null) throw new Exception("Gasto no encontrado.");

            // REGLA DE NEGOCIO: Validar estado de la orden antes de permitir la edición
            if (gasto.OrdenImportacion!.estado != EstadoOrden.Abierta)
            {
                throw new Exception($"No se pueden modificar los gastos porque la orden ya está {gasto.OrdenImportacion.estado}.");
            }

            gasto.MonedaID = dto.MonedaID;
            gasto.Monto = dto.Monto;
            gasto.FechaGasto = dto.FechaGasto;
            // gasto.TipoGasto = dto.TipoGasto; // <- Descomentar si Persona 1 ya lo agregó
            gasto.MetodoDistribucion = dto.MetodoDistribucion;

            _context.GastosImportacion.Update(gasto);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarGastoAsync(int gastoId)
        {
            var gasto = await _context.GastosImportacion
                .Include(g => g.OrdenImportacion)
                .FirstOrDefaultAsync(g => g.Id == gastoId);

            if (gasto == null) throw new Exception("Gasto no encontrado.");

            // REGLA DE NEGOCIO: Validar estado de la orden antes de borrar
            if (gasto.OrdenImportacion!.estado != EstadoOrden.Abierta)
            {
                throw new Exception($"No se pueden eliminar gastos porque la orden ya está {gasto.OrdenImportacion.estado}.");
            }

            _context.GastosImportacion.Remove(gasto);
            await _context.SaveChangesAsync();
        }
    }
}
