using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistance.Entities;

namespace Persistance.EntityConfiguration
{
    public class ResultadoCalculoConfiguration : IEntityTypeConfiguration<ResultadoCalculo>
    {
        public void Configure(EntityTypeBuilder<ResultadoCalculo> builder)
        {
            // 🔑 PK
            builder.HasKey(x => x.Id);

            builder.ToTable("ResultadosCalculo");

            // 🔗 ONE TO ONE → OrdenImportacion
            builder.HasOne(x => x.OrdenImportacion)
                   .WithOne(x => x.ResultadoCalculo) // 👈 debe existir en OrdenImportacion
                   .HasForeignKey<ResultadoCalculo>(x => x.OrdenImportacionId)
                   .OnDelete(DeleteBehavior.Restrict);

            // 🔗 FK → Moneda (MonedaLocal)
            builder.HasOne(x => x.MonedaLocal)
                   .WithMany()
                   .HasForeignKey(x => x.MonedaLocalId)
                   .OnDelete(DeleteBehavior.Restrict);

            // 🔗 FK OPCIONAL → TasaCambio
            builder.HasOne(x => x.TasaCambioUsada)
                   .WithMany()
                   .HasForeignKey(x => x.TasaCambioUsadaId)
                   .IsRequired(false) // ✅ IMPORTANTE
                   .OnDelete(DeleteBehavior.Restrict);

            // 🔗 Relación con Detalles (1 → muchos)
            builder.HasMany(x => x.DetallesResultadoCalculo)
                   .WithOne(x => x.ResultadoCalculo)
                   .HasForeignKey(x => x.ResultadoCalculoId)
                   .OnDelete(DeleteBehavior.Cascade);

            // 🔹 Algunas propiedades (opcional pero recomendado)
            builder.Property(x => x.FOBTotalOriginal).HasColumnType("decimal(18,2)");
            builder.Property(x => x.FOBTotalLocal).HasColumnType("decimal(18,2)");
            builder.Property(x => x.FleteTotal).HasColumnType("decimal(18,2)");
            builder.Property(x => x.SeguroTotal).HasColumnType("decimal(18,2)");
            builder.Property(x => x.CIFTotal).HasColumnType("decimal(18,2)");
            builder.Property(x => x.TotalArancel).HasColumnType("decimal(18,2)");
            builder.Property(x => x.TotalITBIS).HasColumnType("decimal(18,2)");
            builder.Property(x => x.CostoTotalImportacion).HasColumnType("decimal(18,2)");
        }
    }
}
