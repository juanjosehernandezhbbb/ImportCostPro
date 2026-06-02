using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ImportCostPro.Application.Interfaces;
using ImportCostPro.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ImportCostPro.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Módulo de Transacciones y Cálculo
            services.AddScoped<IOrdenImportacionService, OrdenImportacionService>();
            services.AddScoped<IDetalleOrdenService, DetalleOrdenService>();
            services.AddScoped<IGastoImportacionService, GastoImportacionService>();
            services.AddScoped<ILandedCostService, LandedCostService>();

            // Módulo de Catálogos (Mantenimientos)
            services.AddScoped<IMonedaService, MonedaService>();
            services.AddScoped<IProductoService, ProductoService>();
            services.AddScoped<ITasaCambioService, TasaCambioService>();
            services.AddScoped<IPaisService, PaisService>();
            services.AddScoped<IImportadorService, ImportadorService>();
            services.AddScoped<IProveedorService, ProveedorService>();
            services.AddScoped<ICategoriaArancelariaService, CategoriaArancelariaService>();

            return services;
        }
    }
}
