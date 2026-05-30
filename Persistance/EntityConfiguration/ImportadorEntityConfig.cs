using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistance.Entities;

using System;
using System.Collections.Generic;
namespace Persistance.EntityConfiguration
{
    public class ImportadorEntityConfig: IEntityTypeConfiguration<Importador>
    {
        public void Configure(EntityTypeBuilder<Importador> builder)
        {
         //fluent API
            #region basic configuration
            builder.HasKey(x => x.Id);
            builder.ToTable("Importadores");
            #endregion 

            #region Property Configurations
            builder.Property(x => x.NombreRazonSocial)
                .IsRequired()
                .HasMaxLength(150);
            builder.Property(x => x.RNC)
                .IsRequired()
                .HasMaxLength(3);

            builder.HasIndex(x => x.RNC)
            .IsUnique();

            builder.Property(x => x.Estado)
                .IsRequired()
                .HasDefaultValue(true);


            #endregion

            #region Relationship Configurations
            builder.HasOne(x => x.Pais)
                .WithMany(p => p.Importadores)
                .HasForeignKey(x => x.PaisID)
                .OnDelete(DeleteBehavior.Restrict);

            #endregion
        }
    }
}
