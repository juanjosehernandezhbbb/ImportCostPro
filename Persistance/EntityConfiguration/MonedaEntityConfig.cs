using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistance.Entities;

using System;
using System.Collections.Generic;
namespace Persistance.EntityConfiguration
{
    public class MonedaEntityConfig: IEntityTypeConfiguration<Moneda>
    {
        public void Configure(EntityTypeBuilder<Moneda> builder)
        {
         //fluent API
            #region basic configuration
            builder.HasKey(x => x.Id);
            builder.ToTable("Monedas");
            #endregion 

            #region Property Configurations
            builder.Property(x => x.Nombre)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(x => x.CodigoIso)
                .IsRequired()
                .HasMaxLength(3)
                ;
            builder.Property(x=>x.Simbolo)
                .IsRequired()
                .HasMaxLength(3);


            builder.HasIndex(x => x.CodigoIso)
            .IsUnique();

            builder.Property(x => x.Estado)
                .IsRequired();


            #endregion

            #region Relationship Configurations

            #endregion
        }
    }
}
