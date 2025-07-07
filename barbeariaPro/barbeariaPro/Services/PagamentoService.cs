using barbeariaPro.Models;
using barbeariaPro.dbContext;
using Microsoft.EntityFrameworkCore;

namespace barbeariaPro.Services;

public class PagamentoService
{
    private readonly AppDbContext _context;

    public PagamentoService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Pagamento>> ObterTodos()
    {
        return await _context.Pagamento
            .Include(p => p.Agendamento)
            .ToListAsync();
    }

    public async Task<Pagamento?> ObterPorId(int id)
    {
        return await _context.Pagamento
            .Include(p => p.Agendamento)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Pagamento> Adicionar(Pagamento pagamento)
    {
        _context.Pagamento.Add(pagamento);
        await _context.SaveChangesAsync();
        return pagamento;
    }

    public async Task Atualizar(Pagamento pagamento)
    {
        _context.Pagamento.Update(pagamento);
        await _context.SaveChangesAsync();
    }

    public async Task Deletar(Pagamento pagamento)
    {
        _context.Pagamento.Remove(pagamento);
        await _context.SaveChangesAsync();
    }
}
