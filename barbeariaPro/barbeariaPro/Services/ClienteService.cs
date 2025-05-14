using Microsoft.EntityFrameworkCore;

namespace barbeariaPro.Services;

using barbeariaPro.Models;
using barbeariaPro.dbContext;

public class ClienteService
{
    private readonly AppDbContext _context;

    public ClienteService(AppDbContext context) 
    {
        _context = context;
    }

    public async Task<List<Cliente>> ObterTodosClientes()
    {
        return await _context.Clientes.ToListAsync();
    }

    public async Task<Cliente?> ObterClientePorId(int id)
    {
        return await _context.Clientes.FindAsync(id);
    }

    public async Task<Cliente> AdicionarCliente(Cliente novoCliente)
    {
        _context.Clientes.Add(novoCliente);
        await _context.SaveChangesAsync();
        return novoCliente;
    }
    
    public async Task AtualizarCliente(Cliente cliente)
    {
        _context.Clientes.Update(cliente);
        await _context.SaveChangesAsync();
    }

    public async Task DeletarCliente(Cliente cliente)
    {
        _context.Clientes.Remove(cliente);
        await _context.SaveChangesAsync();
    }

}