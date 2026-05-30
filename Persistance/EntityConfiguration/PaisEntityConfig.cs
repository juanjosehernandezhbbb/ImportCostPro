using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistance.Entities;

using System;
using System.Collections.Generic;
namespace Persistance.EntityConfiguration
{
    public class PaisEntityConfig: IEntityTypeConfiguration<Pais>
    {
        public void Configure(EntityTypeBuilder<Pais> builder)
        {
         //fluent API
            #region basic configuration
            builder.HasKey(x => x.Id);
            builder.ToTable("Paises");
            #endregion 

            #region Property Configurations
            builder.Property(x => x.Nombre)
                .IsRequired()
                .HasMaxLength(150);
            builder.Property(x => x.CodigoISO)
                .IsRequired()
                .HasMaxLength(3);

            builder.HasIndex(x => x.CodigoISO)
            .IsUnique();

            builder.Property(x => x.Estado)
                .IsRequired()
                .HasDefaultValue(true);


            #endregion

            #region Relationship Configurations


            #endregion
        }
    }
}
