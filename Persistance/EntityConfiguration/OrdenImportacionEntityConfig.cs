using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistance.Entities;

using System;
using System.Collections.Generic;
namespace Persistance.EntityConfiguration
{
    public class OrdenImportacionEntityConfig: IEntityTypeConfiguration<OrdenImportacion>
    {
        public void Configure(EntityTypeBuilder<OrdenImportacion> builder)
        {
         //fluent API
            #region basic configuration
            builder.HasKey(x => x.Id);
            builder.ToTable("OrdenesImportacion");
            #endregion 

            #region Property Configurations
            builder.Property(x => x.NumeroOrden)
                .IsRequired()
                .HasMaxLength(150);
            builder.Property(x => x.Importador)
                .IsRequired()
                .HasMaxLength(150);
            builder.Property(x=>x.Proveedor)
                .IsRequired()
                .HasMaxLength(150);
            builder.Property(x=>x.PaisOrigen)
                .IsRequired()
                .HasMaxLength(150);
            builder.Property(x => x.Moneda)
                .IsRequired()
                .HasMaxLength(150);
            builder.Property(x => x.FechaOrden)
                .IsRequired();
            builder.Property(x => x.modalidadTransporte)
                .IsRequired();
            builder.Property(x=>x.EstadosOrden)
                .IsRequired();


            


            #endregion

            #region Relationship Configurations
            builder.HasMany(x => x.DetallesOrden)
                .WithOne(x => x.OrdenImportacion)
                .HasForeignKey(oi => oi.OrdenImportacionId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.PaisOrigen)
                .WithMany()
                .HasForeignKey(x => x.PaisOrigenID)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.Moneda)
                .WithMany()
                .HasForeignKey(x => x.MonedaID)
                .OnDelete(DeleteBehavior.Restrict);


            builder.HasOne(x => x.Importador)
                .WithMany()
                .HasForeignKey(x => x.ImportadorID);
            builder.HasMany(x => x.GastosImportacion)
    .WithOne(g => g.OrdenImportacion)
    .HasForeignKey(g => g.OrdenImportacionID)
    .OnDelete(DeleteBehavior.Cascade);


            builder.HasOne(x => x.Proveedor)
    .WithMany()
    .HasForeignKey(x => x.ProveedorID)
    .OnDelete(DeleteBehavior.Restrict);


            #endregion
        }
    }
}
