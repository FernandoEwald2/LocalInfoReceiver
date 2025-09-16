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

            modelBuilder.Entity<Usuario>()
                .HasKey(u => u.Id);
            modelBuilder.Entity<Usuario>()
                .Property(u => u.Nome)
                .IsRequired()
                .HasMaxLength(50);
            modelBuilder.Entity<Usuario>()
                .Property(u => u.Login)
                .IsRequired()
                .HasMaxLength(50);
            modelBuilder.Entity<Usuario>()
                .Property(u => u.SenhaSalt)
                .IsRequired();
            modelBuilder.Entity<Usuario>()
                .Property(u => u.SenhaHash)
                .IsRequired();
        }
    }

}
