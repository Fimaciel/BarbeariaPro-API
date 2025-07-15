using barbeariaPro.Models;
using barbeariaPro.dbContext;
using Microsoft.EntityFrameworkCore;

namespace barbeariaPro.Services;

public class ProfissionalServicoService
{
    private readonly AppDbContext _context;

    public ProfissionalServicoService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<ProfissionalServico>> ObterTodos()
    {
        return await _context.ProfissionalServicos.Include(ps => ps.Profissional).Include(ps => ps.Servico).ToListAsync();
    }

    public async Task<ProfissionalServico?> ObterPorId(int id)
    {
        return await _context.ProfissionalServicos.Include(ps => ps.Profissional).Include(ps => ps.Servico)
                                                  .FirstOrDefaultAsync(ps => ps.Id == id);
    }

    public async Task<ProfissionalServico> Adicionar(ProfissionalServico profissionalServico)
    {
        _context.ProfissionalServicos.Add(profissionalServico);
        await _context.SaveChangesAsync();
        return profissionalServico;
    }

    public async Task Atualizar(ProfissionalServico profissionalServico)
    {
        _context.ProfissionalServicos.Update(profissionalServico);
        await _context.SaveChangesAsync();
    }

    public async Task Deletar(ProfissionalServico profissionalServico)
    {
        _context.ProfissionalServicos.Remove(profissionalServico);
        await _context.SaveChangesAsync();
    }
}
