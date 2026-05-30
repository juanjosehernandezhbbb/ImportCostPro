using System.Text;
using System.Threading.Tasks;
using Persistance.Common;
using Persistance.Enums;
using Persistance.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Persistance.EntityConfiguration
{
    public class GastoImportacionConfiguration : IEntityTypeConfiguration<GastoImportacion>
    {
        public void Configure(EntityTypeBuilder<GastoImportacion> builder)
        {
            builder.HasKey(x => x.Id);

            builder.ToTable("GastosImportacion");

            builder.Property(x => x.Monto)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            builder.Property(x => x.FechaGasto)
                   .IsRequired();

            builder.Property(x => x.OrdenImportacionID)
                   .IsRequired();

            builder.Property(x => x.MonedaID)
                   .IsRequired();


            builder.HasOne(x => x.OrdenImportacion)
                   .WithMany() 
                   .HasForeignKey(x => x.OrdenImportacionID)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Moneda)
                   .WithMany() 
                   .HasForeignKey(x => x.MonedaID)
                   .OnDelete(DeleteBehavior.Restrict);


        }
    }
}
