
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Core.Entities;
using SocialMedia.Infraestructure.Data.Configurations;

namespace SocialMedia.Infraestructure.Data
{
    public partial class RedContext : DbContext
    {
        public RedContext()
        {
        }

        public RedContext(DbContextOptions<RedContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Comment> Comment { get; set; }
        public virtual DbSet<Post> Post { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Security> Seguridad { get; set; } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*asi se hizo al principio por cada configuracio
             * modelBuilder.ApplyConfiguration(new PostConfiguration());
             *modelBuilder.ApplyConfiguration(new CommentConfiguration());
             *modelBuilder.ApplyConfiguration(new UserConfiguration());
            */

            //refactorizacion asi funcinan todas
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }

    }
}
