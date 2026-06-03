using Persistance.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistance.EntityConfiguration
{
    public class GastoImportacionConfiguration : IEntityTypeConfiguration<GastoImportacion>
    {
        public void Configure(EntityTypeBuilder<GastoImportacion> builder)
        {

            builder.ToTable("GastosImportacion");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Monto)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            builder.Property(x => x.FechaGasto)
                   .IsRequired();

            builder.Property(x => x.OrdenImportacionId)
                   .IsRequired();

            builder.Property(x => x.MonedaID)
                   .IsRequired();

            builder.Property(x => x.MetodoDistribucion)
                   .IsRequired();

            builder.Property(x => x.TipoGasto)
                   .IsRequired();

            builder.Property(x => x.Estado)
                   .IsRequired();


            builder.HasOne(x => x.OrdenImportacion)
                   .WithMany(o => o.GastosImportacion)
                   .HasForeignKey(x => x.OrdenImportacionId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Moneda)
                   .WithMany()
                   .HasForeignKey(x => x.MonedaID)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}