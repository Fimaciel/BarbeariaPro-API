using barbeariaPro.dbContext;
using barbeariaPro.Models;
using Microsoft.EntityFrameworkCore;

namespace barbeariaPro.Services;

public class ProfissionalService
{
    private readonly AppDbContext _context;

    public ProfissionalService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Profissional>> ObterTodos()
    {
        return await _context.Profissional
            .Where(p => p.DataExclusao == null)
            .ToListAsync();
    }

    public async Task<Profissional?> ObterPorId(int id)
    {
        return await _context.Profissional
            .FirstOrDefaultAsync(p => p.Id == id && p.DataExclusao == null);
    }

    public async Task<Profissional> Adicionar(Profissional profissional)
    {
        _context.Profissional.Add(profissional);
        await _context.SaveChangesAsync();
        return profissional;
    }

    public async Task Atualizar(Profissional profissional)
    {
        _context.Profissional.Update(profissional);
        await _context.SaveChangesAsync();
    }

    public async Task Deletar(Profissional profissional)
    {
        profissional.DataExclusao = DateTime.UtcNow;
        await _context.SaveChangesAsync();
    }

    public async Task<bool> CpfExiste(string cpf)
    {
        return await _context.Profissional.AnyAsync(p => p.CPF == cpf && p.DataExclusao == null);
    }
}
