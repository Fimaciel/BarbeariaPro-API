using barbeariaPro.Models;
using barbeariaPro.dbContext;
using Microsoft.EntityFrameworkCore;

namespace barbeariaPro.Services;

public class CaixaService
{
    private readonly AppDbContext _context;

    public CaixaService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Caixa>> ObterTodos()
    {
        return await _context.Caixas.Include(c => c.Usuario).ToListAsync();
    }

    public async Task<Caixa?> ObterPorId(int id)
    {
        return await _context.Caixas.Include(c => c.Usuario).FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Caixa> Adicionar(Caixa caixa)
    {
        _context.Caixas.Add(caixa);
        await _context.SaveChangesAsync();
        return caixa;
    }

    public async Task Atualizar(Caixa caixa)
    {
        _context.Caixas.Update(caixa);
        await _context.SaveChangesAsync();
    }

    public async Task Deletar(Caixa caixa)
    {
        _context.Caixas.Remove(caixa);
        await _context.SaveChangesAsync();
    }
}
