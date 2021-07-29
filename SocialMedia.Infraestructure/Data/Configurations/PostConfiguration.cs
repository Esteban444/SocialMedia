using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialMedia.Core.Entities;

namespace SocialMedia.Infraestructure.Data.Configurations
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.ToTable("Post");

            builder.HasKey(e => e.Id);

            /*builder.Property(e => e.Id)
                .HasColumnName("PostId")
                .ValueGeneratedNever();*/

            builder.Property(e => e.Dates).HasColumnType("datetime");

            builder.Property(e => e.Descriptions)
                .IsRequired()
                .HasMaxLength(1000)
                .IsUnicode(false);

            builder.Property(e => e.Images)
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.HasOne(d => d.User)
                .WithMany(p => p.Posts)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Post_Users");
        }
    }
}
