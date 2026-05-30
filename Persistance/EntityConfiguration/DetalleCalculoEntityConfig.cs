using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistance.Entities;

namespace Persistance.EntityConfiguration
{
    public class DetalleCalculoConfiguration : IEntityTypeConfiguration<DetalleCalculo>
    {
        public void Configure(EntityTypeBuilder<DetalleCalculo> builder)
        {
            builder.HasKey(x => x.Id);

            builder.ToTable("DetallesCalculo");

            builder.HasOne(x => x.ResultadoCalculo)
                   .WithMany(x => x.DetallesResultadoCalculo)
                   .HasForeignKey(x => x.ResultadoCalculoId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Producto)
                   .WithMany() 
                   .HasForeignKey(x => x.ProductoId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.CostoFOB)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            builder.Property(x => x.Arancel)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            builder.Property(x => x.ITBIS)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            builder.Property(x => x.CostoTotal)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");
        }
    }
}
