using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMedia.Infraestructure.Data.Configurations
{
    public class SecurityConfiguration: IEntityTypeConfiguration<Security>
    {
        public void Configure(EntityTypeBuilder<Security> builder)
        {
            builder.ToTable("Seguridad");

            builder.HasKey(e => e.Id); 

            builder.Property(e => e.Usuario)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.NombreUsuario)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.Contrasena)
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.Rol)
                .IsRequired()
                .HasMaxLength(50)
                .HasConversion(
                x => x.ToString(),
                x => (RoleType)Enum.Parse(typeof(RoleType), x)
                );
        }
    }
}
