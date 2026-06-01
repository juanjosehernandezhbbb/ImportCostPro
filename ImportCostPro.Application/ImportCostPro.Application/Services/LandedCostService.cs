using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ImportCostPro.Application.DTOs.Calculos;
using ImportCostPro.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using Persistance.Entities;
using Persistance.Enums;

namespace ImportCostPro.Application.Services
{
    public class LandedCostService : ILandedCostService
    {
        private readonly ImportCostProContext _context;

        public LandedCostService(ImportCostProContext context)
        {
            _context = context;
        }

        public async Task<ResultadoCalculoDto> PrevisualizarCalculoAsync(int ordenId)
        {
            // 1. OBTENER LA ORDEN CON TODOS SUS DATOS
            var orden = await _context.OrdenesImportacion
                .Include(o => o.DetallesOrden!)
                    .ThenInclude(d => d.Producto)
                .Include(o => o.GastosImportacion!)
                    .ThenInclude(g => g.Moneda)
                .Include(o => o.Moneda) // La moneda original de la orden (ej. Dólares)
                .FirstOrDefaultAsync(o => o.Id == ordenId);

            if (orden == null) throw new Exception("Orden no encontrada.");
            if (orden.DetallesOrden == null || !orden.DetallesOrden.Any()) throw new Exception("La orden no tiene productos.");
            if (orden.GastosImportacion == null || !orden.GastosImportacion.Any()) throw new Exception("La orden no tiene gastos registrados (Flete/Seguro).");

            // 2. OBTENER LA MONEDA LOCAL Y LA TASA DE CAMBIO
            var monedaLocal = await _context.Monedas.FirstOrDefaultAsync(m => m.EsMonedaLocal == true);
            if (monedaLocal == null) throw new Exception("No hay moneda local configurada en el sistema.");

            decimal tasaDeCambio = 1m; // Por defecto es 1 si la orden ya está en moneda local

            if (orden.MonedaID != monedaLocal.Id)
            {
                // Buscar la tasa vigente más reciente para la fecha de la orden
                var tasa = await _context.TasaCambios
                    .Where(t => t.MonedaOrigenID == orden.MonedaID
                             && t.MonedaDestinoID == monedaLocal.Id
                             && t.FechaVigencia <= orden.FechaOrden)
                    .OrderByDescending(t => t.FechaVigencia)
                    .FirstOrDefaultAsync();

                if (tasa == null) throw new Exception($"No hay tasa de cambio registrada de {orden.Moneda!.Nombre} a la moneda local para la fecha de la orden.");

                tasaDeCambio = tasa.ValorTasa;
            }

            // 3. IDENTIFICAR FLETE Y SEGURO (Asumiendo que TipoGasto enum existe: 1=Flete, 2=Seguro)
            // Si Persona 1 no ha puesto TipoGasto, esto va a fallar. Por ahora sumaremos todo asumiendo que son los totales.
            var fleteTotalLocal = 0m;
            var seguroTotalLocal = 0m;

            foreach (var gasto in orden.GastosImportacion)
            {
                // Convertir cada gasto a moneda local (simplificado: asume que ya están en local o usan la misma tasa)
                decimal gastoEnLocal = gasto.Monto;
                if (gasto.MonedaID != monedaLocal.Id) gastoEnLocal = gasto.Monto * tasaDeCambio;

                // NOTA: Aquí deberías validar el TipoGasto cuando tu compañero lo agregue.
                // if (gasto.TipoGasto == TipoGasto.Flete) fleteTotalLocal += gastoEnLocal;
                // else if (gasto.TipoGasto == TipoGasto.Seguro) seguroTotalLocal += gastoEnLocal;

                // Por ahora, mitad y mitad para que el cálculo corra:
                fleteTotalLocal += gastoEnLocal / 2;
                seguroTotalLocal += gastoEnLocal / 2;
            }

            // 4. CONFIGURACIÓN FISCAL (Aranceles e ITBIS)
            // Usaremos el 18% de ITBIS como ejemplo base, idealmente debe venir de IConfiguracionImpuestosService
            decimal porcentajeItbis = 0.18m;

            // ==========================================
            // COMIENZA EL MOTOR DE CÁLCULO POR PRODUCTO
            // ==========================================

            var resultado = new ResultadoCalculoDto
            {
                OrdenImportacionId = orden.Id,
                NumeroOrden = orden.NumeroOrden,
                FechaCalculo = DateTime.Now,
                TotalFleteLocal = fleteTotalLocal,
                TotalSeguroLocal = seguroTotalLocal
            };

            // Calcular el FOB Total de la orden para poder prorratear "Por Valor FOB"
            decimal fobTotalOrdenMonedaOrden = orden.DetallesOrden.Sum(d => d.Cantidad * d.PrecioUnitarioFOB);
            decimal fobTotalOrdenLocal = fobTotalOrdenMonedaOrden * tasaDeCambio;
            resultado.TotalFobConvertidoLocal = fobTotalOrdenLocal;

            foreach (var detalle in orden.DetallesOrden)
            {
                var rProducto = new DetalleResultadoProductoDto
                {
                    ProductoId = detalle.ProductoId,
                    NombreProducto = detalle.Producto!.Nombre,
                    Cantidad = detalle.Cantidad,
                    PrecioFobUnitarioMonedaOrden = detalle.PrecioUnitarioFOB,
                    MargenGananciaDeseado = detalle.MargenDeseado
                };

                // PASO 1: FOB Local
                rProducto.PrecioFobTotalMonedaLocal = (rProducto.Cantidad * rProducto.PrecioFobUnitarioMonedaOrden) * tasaDeCambio;

                // PASO 2: Prorrateo (Qué pedazo de flete y seguro le toca). Usaremos Método "Por Valor FOB" por defecto.
                rProducto.PorcentajeParticipacion = (rProducto.PrecioFobTotalMonedaLocal / fobTotalOrdenLocal);
                rProducto.FleteAsignadoLocal = fleteTotalLocal * rProducto.PorcentajeParticipacion;
                rProducto.SeguroAsignadoLocal = seguroTotalLocal * rProducto.PorcentajeParticipacion;

                // PASO 3: Cálculo del CIF (FOB + Flete + Seguro)
                rProducto.ValorCifLocal = rProducto.PrecioFobTotalMonedaLocal + rProducto.FleteAsignadoLocal + rProducto.SeguroAsignadoLocal;

                // PASO 4: Aranceles (Aquí deberías buscar el Arancel en la tabla Producto -> CategoriaArancelaria)
                // Por ahora, asumimos un 20% genérico para probar el motor
                decimal porcentajeArancel = 0.20m;
                rProducto.ArancelAplicado = rProducto.ValorCifLocal * porcentajeArancel;

                // PASO 5: Base imponible y cálculo del ITBIS
                decimal baseItbis = rProducto.ValorCifLocal + rProducto.ArancelAplicado;
                rProducto.ItbisAplicado = baseItbis * porcentajeItbis;

                // PASO 6: Costo Total y Unitario Importado
                rProducto.CostoTotalImportadoLocal = rProducto.ValorCifLocal + rProducto.ArancelAplicado + rProducto.ItbisAplicado; // Faltaría TasaServicio
                rProducto.CostoUnitarioFinalLocal = rProducto.CostoTotalImportadoLocal / rProducto.Cantidad;

                // PASO 7: Precio Sugerido de Venta (Usando el Margen)
                // Fórmula de margen: Costo / (1 - (Margen / 100))
                decimal margenDecimal = rProducto.MargenGananciaDeseado / 100m;
                if (margenDecimal >= 1) margenDecimal = 0.99m; // Protección para evitar división por cero

                rProducto.PrecioVentaSugerido = rProducto.CostoUnitarioFinalLocal / (1 - margenDecimal);

                // Agregar a la lista y sumar a los totales generales
                resultado.DetallesPorProducto.Add(rProducto);
                resultado.TotalCifLocal += rProducto.ValorCifLocal;
                resultado.TotalAranceles += rProducto.ArancelAplicado;
                resultado.TotalItbis += rProducto.ItbisAplicado;
            }

            resultado.GranTotalLandedCost = resultado.TotalCifLocal + resultado.TotalAranceles + resultado.TotalItbis;

            return resultado;
        }

        public async Task<int> GuardarCalculoOficialAsync(int ordenId)
        {
            // 1. Ejecutamos el motor matemático en memoria que ya construimos
            var resultadoPrevisualizado = await PrevisualizarCalculoAsync(ordenId);

            // 2. Buscamos la orden para cambiarle el estado
            var orden = await _context.OrdenesImportacion.FindAsync(ordenId);
            if (orden == null) throw new Exception("Orden no encontrada.");

            if (orden.estado != EstadoOrden.Abierta)
            {
                throw new Exception($"No se puede guardar un cálculo oficial si la orden está en estado {orden.estado}.");
            }

            // Buscamos la moneda local (para rellenar los datos de la entidad)
            var monedaLocal = await _context.Monedas.FirstOrDefaultAsync(m => m.EsMonedaLocal == true);

            // 3. INICIAMOS UNA TRANSACCIÓN
            // Si algo falla al guardar el resultado o al actualizar la orden, se cancela TODO.
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // 4. Mapeamos los datos al registro maestro (ResultadoCalculo)
                var calculoOficial = new ResultadoCalculo
                {
                    Id = 0,
                    Estado = true,
                    OrdenImportacionId = ordenId,
                    MonedaLocalId = monedaLocal!.Id,
                    TasaCambioUsadaId = null, // Aquí podrías poner el ID de la tasa vigente si la buscaste
                    FOBTotalOriginal = resultadoPrevisualizado.TotalFobConvertidoLocal,
                    FOBTotalLocal = resultadoPrevisualizado.TotalFobConvertidoLocal,
                    FleteTotal = resultadoPrevisualizado.TotalFleteLocal,
                    SeguroTotal = resultadoPrevisualizado.TotalSeguroLocal,
                    CIFTotal = resultadoPrevisualizado.TotalCifLocal,
                    TotalArancel = resultadoPrevisualizado.TotalAranceles,
                    TotalITBIS = resultadoPrevisualizado.TotalItbis,
                    CostoTotalImportacion = resultadoPrevisualizado.GranTotalLandedCost,
                    PorcentajeITBIS = 0.18m, // Usamos el mismo 18% del ejemplo
                    PorcentajeTasaAduanal = 0,
                    FechaCalculo = DateTime.Now,
                    DetallesResultadoCalculo = new List<DetalleCalculo>()
                };

                // 5. Mapeamos los productos al detalle de la fotografía
                foreach (var itemProducto in resultadoPrevisualizado.DetallesPorProducto)
                {
                    var detalle = new DetalleCalculo
                    {
                        // Nota: DetalleCalculo no hereda de BasicEntity, así que no lleva Estado
                        ResultadoCalculo = calculoOficial, // Para satisfacer el "required"
                        ProductoId = itemProducto.ProductoId,
                        Producto = null!, // Truco (null forgiving) para calmar el "required" sin tener que hacer una consulta extra a BD
                        CostoFOB = itemProducto.PrecioFobTotalMonedaLocal,
                        Arancel = itemProducto.ArancelAplicado,
                        ITBIS = itemProducto.ItbisAplicado,
                        CostoTotal = itemProducto.CostoTotalImportadoLocal
                    };
                    calculoOficial.DetallesResultadoCalculo.Add(detalle);
                }

                // Guardamos el cálculo y sus detalles
                _context.ResultadosCalculo.Add(calculoOficial);

                // 6. ACTUALIZAMOS EL ESTADO DE LA ORDEN
                orden.estado = EstadoOrden.Calculada;
                _context.OrdenesImportacion.Update(orden);

                // 7. Aplicamos los cambios a la base de datos
                await _context.SaveChangesAsync();

                // Confirmamos la transacción
                await transaction.CommitAsync();

                return calculoOficial.Id;
            }
            catch (Exception ex)
            {
                // Si algo explota (ej. se va la luz en el servidor de BD), deshacemos los cambios
                await transaction.RollbackAsync();
                throw new Exception("Ocurrió un error al guardar el cálculo oficial. Se han revertido los cambios.", ex);
            }
        }
    }
}
