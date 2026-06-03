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
        public DbSet<DetalleOrden> DetallesOrden { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<GastoImportacion> GastosImportacion { get; set; }
        public DbSet<ResultadoCalculo> ResultadosCalculo { get; set; }
        public DbSet<DetalleCalculo> DetallesCalculo { get; set; }
        public DbSet<TasaCambio> TasaCambios { get; set; }
        public DbSet<ConfiguracionImpuestos> ConfiguracionImpuestos { get; set; }
        public DbSet<CategoriaArancelaria> CategoriaArancelarias { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Aplica configuraciones por reflexión (si tienes archivos de configuración separados)
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            // --- CONFIGURACIÓN DE IMPORTADOR (Forzada para evitar errores de truncamiento) ---
            modelBuilder.Entity<Importador>(entity =>
            {
                entity.ToTable("Importadores");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.NombreRazonSocial).IsRequired().HasMaxLength(150);
                entity.Property(e => e.RNC).IsRequired().HasMaxLength(21); // Tamaño exacto corregido
                entity.Property(e => e.Telefono).HasMaxLength(20);
                entity.Property(e => e.Correo).HasMaxLength(100);
                entity.Property(e => e.Direccion).HasMaxLength(250);
            });

            // --- CONFIGURACIÓN DE PRODUCTO ---
            modelBuilder.Entity<Producto>(entity =>
            {
                entity.ToTable("Productos");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(150);
                entity.Property(e => e.Codigo).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Descripcion).HasMaxLength(500);
            });

            // --- CONFIGURACIÓN DE DETALLEORDEN ---
            modelBuilder.Entity<DetalleOrden>(entity =>
            {
                entity.ToTable("DetallesOrden");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Descripcion).IsRequired().HasMaxLength(200);
            });
        }
    }
}