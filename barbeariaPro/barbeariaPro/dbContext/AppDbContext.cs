using Microsoft.EntityFrameworkCore;

namespace barbeariaPro.dbContext;
using barbeariaPro.Models;

public class AppDbContext :  Microsoft.EntityFrameworkCore.DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}
    
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Agendamento> Agendamentos { get; set; }
    
    public DbSet<Caixa> Caixas { get; set; }
    
    public DbSet<MovimentacaoCaixa> MovimentacaoCaixa { get; set; }
    
    public DbSet<Pagamento> Pagamento { get; set; }
    
    public DbSet<Profissional> Profissional { get; set; }
    
    public DbSet<ProfissionalServico> ProfissionalServico { get; set; }
    
    public DbSet<Servico> Servico { get; set; }
    
    public DbSet<Usuario> Usuario { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Profissional>()
            .HasOne(p => p.Usuario)
            .WithOne(u => u.Profissional)
            .HasForeignKey<Profissional>(p => p.Id);
    }
}