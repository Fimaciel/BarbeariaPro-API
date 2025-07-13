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

    public async Task<List<MovimentacaoCaixa>> ObterTodos()
    {
        return await _context.MovimentacoesCaixa.Include(m => m.Caixa).ToListAsync();
    }

    public async Task<MovimentacaoCaixa?> ObterPorId(int id)
    {
        return await _context.MovimentacoesCaixa.Include(m => m.Caixa).FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<MovimentacaoCaixa> Adicionar(MovimentacaoCaixa movimentacao)
    {
        _context.MovimentacoesCaixa.Add(movimentacao);
        await _context.SaveChangesAsync();
        return movimentacao;
    }

    public async Task Atualizar(MovimentacaoCaixa movimentacao)
    {
        _context.MovimentacoesCaixa.Update(movimentacao);
        await _context.SaveChangesAsync();
    }

    public async Task Deletar(MovimentacaoCaixa movimentacao)
    {
        _context.MovimentacoesCaixa.Remove(movimentacao);
        await _context.SaveChangesAsync();
    }
    public async Task<List<MovimentacaoCaixa>> ObterPorCaixa(int caixaId)
    {
        return await _context.MovimentacoesCaixa
            .Where(m => m.CaixaFk == caixaId)
            .ToListAsync();
    }

}
