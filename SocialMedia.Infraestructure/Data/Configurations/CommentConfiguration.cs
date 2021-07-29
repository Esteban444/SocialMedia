using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialMedia.Core.Entities;


namespace SocialMedia.Infraestructure.Data.Configurations
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("Comment");

            builder.HasKey(e => e.Id);

            /*builder.Property(e => e.Id)
                .HasColumnName("CommentId")
                .ValueGeneratedNever();*/

            builder.Property(e => e.Dates).HasColumnType("datetime");

                builder.Property(e => e.Descriptions)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                builder.HasOne(d => d.Post)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Comment_Post");

                builder.HasOne(d => d.User)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Comment_Users");
            
        }
    }
}
