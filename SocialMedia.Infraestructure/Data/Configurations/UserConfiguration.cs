using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialMedia.Core.Entities;

namespace SocialMedia.Infraestructure.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<Users>
    {
        public void Configure(EntityTypeBuilder<Users> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(e => e.Id)
                    .HasName("PK__Users__1788CC4CE2285DCD");

            /*builder.Property(e => e.Id)
                .HasColumnName("UserId")
                .ValueGeneratedNever();*/

            builder.Property(e => e.DateOfBirth).HasColumnType("date");

            builder.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(30)
                .IsUnicode(false);

            builder.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.Telephone)
                .HasMaxLength(10)
                .IsUnicode(false);
        }
    }
}
