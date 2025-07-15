using barbeariaPro.Models;
using barbeariaPro.dbContext;
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
        return await _context.Profissionais.Include(p => p.Usuario).Where(p => p.DataExclusao == null).ToListAsync();
    }

    public async Task<Profissional?> ObterPorId(int id)
    {
        return await _context.Profissionais.Include(p => p.Usuario).FirstOrDefaultAsync(p => p.Id == id && p.DataExclusao == null);
    }

    public async Task<Profissional> Adicionar(Profissional profissional)
    {
        _context.Profissionais.Add(profissional);
        await _context.SaveChangesAsync();
        return profissional;
    }

    public async Task Atualizar(Profissional profissional)
    {
        _context.Profissionais.Update(profissional);
        await _context.SaveChangesAsync();
    }

    public async Task Deletar(Profissional profissional)
    {
        profissional.DataExclusao = DateTime.UtcNow;
        await _context.SaveChangesAsync();
    }
}
