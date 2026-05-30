using Microsoft.EntityFrameworkCore;
using Persistance.Entities;
using System.Reflection;

namespace Persistence.Contexts
{
    public class ImportCostProContext : DbContext
    {
        public ImportCostProContext(DbContextOptions<ImportCostProContext> options) : base(options) { }

        public DbSet<Pais> Paises { get; set; }
        public DbSet<Moneda> Monedas { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<Importador> Importadores { get; set; }
        public DbSet<OrdenImportacion> OrdenesImportacion { get; set; }
        public DbSet<DetalleOrden> DetallesOrdenImportacion { get; set; }
        public DbSet<GastoImportacion> GastosImportacion { get; set; }
        public DbSet<ResultadoCalculo> ResultadosCalculo { get; set; }
        public DbSet<DetalleCalculo> DetallesCalculo { get; set; }
        public DbSet<TasaCambio> TasaCambios { get; set; }
        public DbSet<GastoImportacion> GastoImportaciones { get; set; }
        public DbSet<ConfiguracionImpuestos> ConfiguracionImpuestos { get; set; }
        public DbSet<CategoriaArancelaria> CategoriaArancelarias { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); //Liskov-substitution

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
