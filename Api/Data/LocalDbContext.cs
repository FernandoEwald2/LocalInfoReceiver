using Api.Domain.Entities.Usuarios;
using Microsoft.EntityFrameworkCore;

namespace Api.Data
{
    public class LocalDbContext : DbContext
    {
        public LocalDbContext(DbContextOptions<LocalDbContext> options) : base(options) { }

        public virtual DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // aplica automaticamente todas as classes que implementam IEntityTypeConfiguration
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(LocalDbContext).Assembly);
        }
    }
}
