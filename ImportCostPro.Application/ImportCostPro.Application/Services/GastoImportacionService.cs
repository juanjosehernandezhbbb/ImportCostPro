using System;
using System.Collections.Generic;
using System.Linq;
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
            // 1. Verificamos la orden sin cargar el objeto de navegación completo si no es necesario
            var orden = await _context.OrdenesImportacion.AsNoTracking().FirstOrDefaultAsync(o => o.Id == dto.OrdenImportacionId);

            if (orden == null) throw new Exception("La orden de importación no existe.");

            if (orden.estado != EstadoOrden.Abierta)
            {
                throw new Exception($"No se pueden registrar gastos en una orden que está en estado {orden.estado}.");
            }

            // 2. Creamos la entidad
            var nuevoGasto = new GastoImportacion
            {
                OrdenImportacionId = dto.OrdenImportacionId,
                MonedaID = dto.MonedaID,
                Monto = dto.Monto,
                FechaGasto = dto.FechaGasto,
                TipoGasto = dto.TipoGasto,
                MetodoDistribucion = dto.MetodoDistribucion,
                Estado = true
            };

            // 3. Importante: Al no asignar el objeto 'nuevoGasto.OrdenImportacion = orden', 
            // evitamos que EF intente gestionar una relación que ya está resuelta por el ID.
            _context.GastosImportacion.Add(nuevoGasto);
            await _context.SaveChangesAsync();

            return nuevoGasto.Id;
        }

        public async Task<IEnumerable<GastoImportacionDto>> ObtenerPorOrdenIdAsync(int ordenId)
        {
            return await _context.GastosImportacion
                .AsNoTracking() // Optimización para lectura
                .Include(g => g.Moneda)
                .Where(g => g.OrdenImportacionId == ordenId)
                .Select(g => new GastoImportacionDto
                {
                    Id = g.Id,
                    OrdenImportacionId = g.OrdenImportacionId,
                    MonedaID = g.MonedaID,
                    NombreMoneda = g.Moneda!.Nombre,
                    SimboloMoneda = g.Moneda!.Simbolo,
                    Monto = g.Monto,
                    FechaGasto = g.FechaGasto,
                    TipoGasto = g.TipoGasto,
                    MetodoDistribucion = g.MetodoDistribucion,
                    Estado = g.Estado
                })
                .ToListAsync();
        }

        public async Task ActualizarGastoAsync(int id, AgregarGastoDto dto)
        {
            var gasto = await _context.GastosImportacion
                .Include(g => g.OrdenImportacion)
                .FirstOrDefaultAsync(g => g.Id == id);

            if (gasto == null) throw new Exception("Gasto no encontrado.");

            if (gasto.OrdenImportacion!.estado != EstadoOrden.Abierta)
            {
                throw new Exception($"No se pueden modificar los gastos porque la orden ya está {gasto.OrdenImportacion.estado}.");
            }

            gasto.MonedaID = dto.MonedaID;
            gasto.Monto = dto.Monto;
            gasto.FechaGasto = dto.FechaGasto;
            gasto.TipoGasto = dto.TipoGasto;
            gasto.MetodoDistribucion = dto.MetodoDistribucion;
            gasto.Estado = true;

            await _context.SaveChangesAsync();
        }

        public async Task EliminarGastoAsync(int gastoId)
        {
            var gasto = await _context.GastosImportacion
                .Include(g => g.OrdenImportacion)
                .FirstOrDefaultAsync(g => g.Id == gastoId);

            if (gasto == null) throw new Exception("Gasto no encontrado.");

            if (gasto.OrdenImportacion!.estado != EstadoOrden.Abierta)
            {
                throw new Exception($"No se pueden eliminar gastos porque la orden ya está {gasto.OrdenImportacion.estado}.");
            }

            _context.GastosImportacion.Remove(gasto);
            await _context.SaveChangesAsync();
        }
    }
}