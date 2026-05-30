using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistance.Entities;

using System;
using System.Collections.Generic;
namespace Persistance.EntityConfiguration
{
    public class ConfiguracionImpuestosEntityConfig: IEntityTypeConfiguration<ConfiguracionImpuestos>
    {
        public void Configure(EntityTypeBuilder<ConfiguracionImpuestos> builder)
        {
         //fluent API
            #region basic configuration
            builder.HasKey(x => x.Id);
            builder.ToTable("ConfiguracionesImpuestos");
            #endregion 

            #region Property Configurations
            builder.Property(x => x.Id)
                .IsRequired();
            builder.Property(x => x.PorcentajeITBIS)
                .IsRequired()
                .HasPrecision(5, 2);
            builder.Property(x => x.PorcentajeTasaAduanal)
                .IsRequired()
                .HasPrecision(18, 2);
            builder.HasIndex(x => x.Id)
             .IsUnique();

            #endregion

            #region Relationship Configurations


            #endregion
        }
    }
}
