using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistance.Entities;

using System;
using System.Collections.Generic;
namespace Persistance.EntityConfiguration
{
    public class CategoriaArancelariaEntityConfig: IEntityTypeConfiguration<CategoriaArancelaria>
    {
        public void Configure(EntityTypeBuilder<CategoriaArancelaria> builder)
        {
         //fluent API
            #region basic configuration
            builder.HasKey(x => x.Id);
            builder.ToTable("CategoriasArancelarias");
            #endregion

            #region Property Configurations

            builder.Property(x => x.CodigoArancelio)
                        .IsRequired()
                        .HasMaxLength(20);

            builder.HasIndex(x => x.CodigoArancelio)
                .IsUnique();

            builder.Property(x => x.Nombre)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(x => x.PorcentajeArancel)
                .IsRequired()
                .HasPrecision(18, 2);

            builder.Property(x => x.AplicaITBS)
                .IsRequired();

            builder.Property(x => x.AplicaSelectivo)
                .IsRequired();

            builder.Property(x => x.PorctImptSelectivo)
                .HasPrecision(18, 2);

            builder.Property(x => x.Estado)
                .IsRequired();

            builder.Property(x => x.Estado)
                      .IsRequired()
                      .HasDefaultValue(true);



            #endregion

            #region Relationship Configurations


            #endregion
        }
    }
}
