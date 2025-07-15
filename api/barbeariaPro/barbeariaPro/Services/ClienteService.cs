using Microsoft.EntityFrameworkCore;
using barbeariaPro.Models;
using barbeariaPro.dbContext;

namespace barbeariaPro.Services;

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
        return await _context.Clientes
                             .FirstOrDefaultAsync(c => c.Id == id); 
    }

    public async Task<Cliente> AdicionarCliente(Cliente novoCliente)
    {
        if (await CpfExiste(novoCliente.Cpf))
        {
            throw new Exception("Já existe um cliente com esse CPF.");
        }

        _context.Clientes.Add(novoCliente);
        await _context.SaveChangesAsync();
        return novoCliente;
    }

    public async Task AtualizarCliente(Cliente cliente)
    {
        var clienteExistente = await ObterClientePorId(cliente.Id);
        if (clienteExistente == null)
        {
            throw new Exception("Cliente não encontrado.");
        }

        _context.Clientes.Update(cliente);
        await _context.SaveChangesAsync();
    }

    public async Task DeletarCliente(Cliente cliente)
    {
        var clienteExistente = await ObterClientePorId(cliente.Id);
        if (clienteExistente == null)
        {
            throw new Exception("Cliente não encontrado.");
        }

        _context.Clientes.Remove(cliente);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> CpfExiste(string? cpf)
    {
        if (string.IsNullOrWhiteSpace(cpf)) return false;

        return await _context.Clientes.AnyAsync(c => c.Cpf == cpf);
    }
}
