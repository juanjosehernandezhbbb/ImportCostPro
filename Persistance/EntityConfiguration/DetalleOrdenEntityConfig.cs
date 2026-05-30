using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistance.Entities;

using System;
using System.Collections.Generic;
namespace Persistance.EntityConfiguration
{
    public class DetalleOrdenEntityConfig : IEntityTypeConfiguration<DetalleOrden>
    {
        public void Configure(EntityTypeBuilder<DetalleOrden> builder)
        {
            //fluent API
            #region basic configuration
            builder.HasKey(x => x.Id);
            builder.ToTable("DetallesOrden");
            #endregion

            #region Property Configurations

            builder.Property(x => x.OrdenImportacionId)
                .IsRequired();

            builder.Property(x => x.ProductoId)
                .IsRequired();

            builder.Property(x => x.Cantidad)
                .IsRequired()
                .HasPrecision(18, 2);

            builder.Property(x => x.PrecioUnitarioFOB)
                .IsRequired()
                .HasPrecision(18, 2);

            builder.Property(x => x.MargenDeseado)
                .IsRequired()
                .HasPrecision(5, 2);

            builder.Property(x => x.Descripcion)
                .HasMaxLength(200);

            builder.Property(x => x.Estado)
                .IsRequired()
                .HasDefaultValue(true);

            builder.HasIndex(x => new { x.OrdenImportacionId, x.ProductoId })
                .IsUnique();

            #endregion

            #region Relationship Configurations

            builder.HasOne(x => x.OrdenImportacion)
                .WithMany()
                .HasForeignKey(x => x.OrdenImportacionId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Producto)
                .WithMany()
                .HasForeignKey(x => x.ProductoId)
                .OnDelete(DeleteBehavior.Restrict);

            #endregion


        }
    }
}

