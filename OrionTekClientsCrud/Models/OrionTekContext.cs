using Microsoft.EntityFrameworkCore;

namespace OrionTekClientsCrud.Models
{
    public class OrionTekContext : DbContext
    {
        public OrionTekContext(DbContextOptions<OrionTekContext> options) : base(options) { }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Direccion> Direcciones { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>()
                .HasMany(c => c.Direcciones)
                .WithOne(d => d.Cliente)
                .IsRequired();
        }
    }
}
