using Microsoft.EntityFrameworkCore;

namespace barbeariaPro.dbContext;
using barbeariaPro.Models;

public class AppDbContext :  Microsoft.EntityFrameworkCore.DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}
    
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Agendamento> Agendamentos { get; set; }
    
    public DbSet<Caixa> Caixas { get; set; }
    
    public DbSet<MovimentacaoCaixa> MovimentacoesCaixa { get; set; }
    
    public DbSet<Pagamento> Pagamentos { get; set; }
    
    public DbSet<Profissional> Profissionais { get; set; }
    
    public DbSet<ProfissionalServico> ProfissionalServicos { get; set; }
    
    public DbSet<Servico> Servicos { get; set; }
    
    public DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Usuario>()
            .HasOne(u => u.Profissional)
            .WithOne(p => p.Usuario)
            .HasForeignKey<Usuario>(u => u.ProfissionalFk)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Agendamento>()
            .HasOne(a => a.Profissional)
            .WithMany(p => p.Agendamentos)
            .HasForeignKey(a => a.ProfissionalFk)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Agendamento>()
            .HasOne(a => a.Cliente)
            .WithMany()
            .HasForeignKey(a => a.ClienteFk)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Agendamento>()
            .HasOne(a => a.Servico)
            .WithMany(s => s.Agendamentos)
            .HasForeignKey(a => a.ServicoFk)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Pagamento>()
            .HasOne(p => p.Agendamento)
            .WithMany(a => a.Pagamentos)
            .HasForeignKey(p => p.AgendamentoFk)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Caixa>()
            .HasOne(c => c.Usuario)
            .WithMany()
            .HasForeignKey(c => c.UsuarioFk)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<MovimentacaoCaixa>()
            .HasOne(m => m.Caixa)
            .WithMany(c => c.Movimentacoes)
            .HasForeignKey(m => m.CaixaFk)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ProfissionalServico>()
            .HasKey(ps => ps.Id);

        modelBuilder.Entity<ProfissionalServico>()
            .HasOne(ps => ps.Profissional)
            .WithMany(p => p.Servicos)
            .HasForeignKey(ps => ps.ProfissionalFk)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ProfissionalServico>()
            .HasOne(ps => ps.Servico)
            .WithMany(s => s.Profissionais)
            .HasForeignKey(ps => ps.ServicoFk)
            .OnDelete(DeleteBehavior.Cascade);
    }
}