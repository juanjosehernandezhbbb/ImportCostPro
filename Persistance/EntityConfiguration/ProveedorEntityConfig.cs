using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistance.Entities;

using System;
using System.Collections.Generic;
namespace Persistance.EntityConfiguration
{
    public class ProveedorEntityConfig: IEntityTypeConfiguration<Proveedor>
    {
        public void Configure(EntityTypeBuilder<Proveedor> builder)
        {
         //fluent API
            #region basic configuration
            builder.HasKey(x => x.Id);
            builder.ToTable("Proveedores");
            #endregion 

            #region Property Configurations
            builder.Property(x => x.Nombre)
                .IsRequired()
                .HasMaxLength(150);
            builder.Property(x => x.PaisOrigenID)
                .IsRequired();

            builder.HasIndex(x => x.PaisOrigenID)
            .IsUnique();

            builder.Property(x => x.Estado)
                .IsRequired()
                .HasDefaultValue(true);
            builder.Property(x => x.MonedaPrincipalID)
                .IsRequired();


            #endregion

            #region Relationship Configurations
            builder.HasOne(x => x.PaisOrigen)
                .WithMany()
                .HasForeignKey(x => x.PaisOrigenID)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.MonedaPrincipal)
            .WithMany()
            .HasForeignKey(x => x.MonedaPrincipalID)
            .OnDelete(DeleteBehavior.Restrict);


            #endregion
        }
    }
}
