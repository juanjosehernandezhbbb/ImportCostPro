using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistance.Entities;

using System;
using System.Collections.Generic;
namespace Persistance.EntityConfiguration
{
    public class TasaCambioEntityConfig: IEntityTypeConfiguration<TasaCambio>
    {
        public void Configure(EntityTypeBuilder<TasaCambio> builder)
        {
            //fluent API
            #region basic configuration
            builder.HasKey(x => x.Id);
            builder.ToTable("TasasCambio");

            #endregion 

            #region Property Configurations
            builder.Property(x => x.MonedaOrigenID)
                .IsRequired();
            builder.Property(x => x.MonedaDestinoID)
                .IsRequired(); 
            builder.Property(x => x.ValorTasa)
                .IsRequired()
                .HasPrecision(18, 4);

            builder.Property(x => x.FechaVigencia)
                .IsRequired();
         
            builder.Property(x => x.Estado)
                .IsRequired()
                .HasDefaultValue(true);


            #endregion

            #region Relationship Configurations
            builder.HasOne(tc => tc.MonedaOrigen)
                .WithMany()
                .HasForeignKey(mo => mo.MonedaOrigenID)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(tc => tc.MonedaDestino)
                .WithMany()
                .HasForeignKey(md => md.MonedaDestinoID)
                .OnDelete(DeleteBehavior.Restrict);

                

            #endregion
        }
    }
}
