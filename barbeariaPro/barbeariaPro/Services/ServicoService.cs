using barbeariaPro.Models;
using barbeariaPro.dbContext;
using Microsoft.EntityFrameworkCore;

namespace barbeariaPro.Services;

public class ServicoService
{
    private readonly AppDbContext _context;

    public ServicoService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Servico>> ObterTodos()
    {
        return await _context.Servico
            .Where(s => s.DataExclusao == null)
            .ToListAsync();
    }

    public async Task<Servico?> ObterPorId(int id)
    {
        return await _context.Servico
            .FirstOrDefaultAsync(s => s.Id == id && s.DataExclusao == null);
    }

    public async Task<Servico> Adicionar(Servico servico)
    {
        _context.Servico.Add(servico);
        await _context.SaveChangesAsync();
        return servico;
    }

    public async Task Atualizar(Servico servico)
    {
        _context.Servico.Update(servico);
        await _context.SaveChangesAsync();
    }

    public async Task Deletar(Servico servico)
    {
        servico.DataExclusao = DateTime.UtcNow;
        await _context.SaveChangesAsync();
    }
}
