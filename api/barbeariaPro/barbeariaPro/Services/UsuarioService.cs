using barbeariaPro.Models;
using barbeariaPro.dbContext;
using Microsoft.EntityFrameworkCore;

namespace barbeariaPro.Services;

public class UsuarioService
{
    private readonly AppDbContext _context;

    public UsuarioService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Usuario>> ObterTodos()
    {
        return await _context.Usuarios.Include(u => u.Profissional).ToListAsync();
    }

    public async Task<Usuario?> ObterPorId(int id)
    {
        return await _context.Usuarios.Include(u => u.Profissional).FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<Usuario> Adicionar(Usuario usuario)
    {
        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();
        return usuario;
    }

    public async Task Atualizar(Usuario usuario)
    {
        _context.Usuarios.Update(usuario);
        await _context.SaveChangesAsync();
    }

    public async Task Deletar(Usuario usuario)
    {
        _context.Usuarios.Remove(usuario);
        await _context.SaveChangesAsync();
    }
}
