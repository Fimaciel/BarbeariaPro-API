using barbeariaPro.Models;
using barbeariaPro.dbContext;
using Microsoft.EntityFrameworkCore;

namespace barbeariaPro.Services;

public class MovimentacaoCaixaService
{
    private readonly AppDbContext _context;

    public MovimentacaoCaixaService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<MovimentacaoCaixa>> ObterTodas()
    {
        return await _context.MovimentacaoCaixa
            .Include(m => m.Caixa)
            .ToListAsync();
    }

    public async Task<MovimentacaoCaixa?> ObterPorId(int id)
    {
        return await _context.MovimentacaoCaixa
            .Include(m => m.Caixa)
            .FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<MovimentacaoCaixa> Adicionar(MovimentacaoCaixa movimentacao)
    {
        _context.MovimentacaoCaixa.Add(movimentacao);
        await _context.SaveChangesAsync();
        return movimentacao;
    }

    public async Task Atualizar(MovimentacaoCaixa movimentacao)
    {
        _context.MovimentacaoCaixa.Update(movimentacao);
        await _context.SaveChangesAsync();
    }

    public async Task Deletar(MovimentacaoCaixa movimentacao)
    {
        _context.MovimentacaoCaixa.Remove(movimentacao);
        await _context.SaveChangesAsync();
    }
}
