using Microsoft.EntityFrameworkCore;
using SalaReunioes.Domain.Entities;

namespace SalaReunioes.Infrastructure.Data;

public class AppDbContext : DbContext {
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<Sala> Salas { get; set; }
    public DbSet<Agendamento> Agendamentos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Entity<Agendamento>()
            .HasIndex(a => new { a.SalaId, a.Inicio, a.Fim });
    }
}