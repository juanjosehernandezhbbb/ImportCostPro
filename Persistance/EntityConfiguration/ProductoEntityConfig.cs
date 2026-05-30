using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistance.Entities;

using System;
using System.Collections.Generic;
namespace Persistance.EntityConfiguration
{
    public class ProductoEntityConfig: IEntityTypeConfiguration<Producto>
    {
        public void Configure(EntityTypeBuilder<Producto> builder)
        {
         //fluent API
            #region basic configuration
            builder.HasKey(x => x.Id);
            builder.ToTable("Productos");
            #endregion 

            #region Property Configurations
            builder.Property(x => x.Nombre)
                .IsRequired()
                .HasMaxLength(150);
            builder.Property(x => x.Codigo)
                .IsRequired()
                .HasMaxLength(3);

            builder.HasIndex(x => x.Codigo)
            .IsUnique();

            builder.Property(x => x.Estado)
                .IsRequired()
                .HasDefaultValue(true);
            builder.Property(x => x.PesoUnitario)
                .IsRequired()
                .HasPrecision(18, 2);
            builder.Property(x=>x.PaisOrigenID)
                .IsRequired();
            builder.Property(x=>x.CategoriaArancelariaID)
                .IsRequired();
            builder.Property(x=>x.UnidadDeMedidaID)
                .IsRequired();

            #endregion

            #region Relationship Configurations
            builder.HasOne(x => x.PaisOrigen)
                .WithMany()
                .HasForeignKey(x => x.PaisOrigenID);
            builder.HasOne(x => x.CategoriaArancelaria)
                .WithMany()
                .HasForeignKey(x => x.CategoriaArancelariaID)
                .OnDelete(DeleteBehavior.Restrict); 


            #endregion
        }
    }
}
