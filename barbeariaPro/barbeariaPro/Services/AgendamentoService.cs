using barbeariaPro.Models;
using barbeariaPro.dbContext;
using Microsoft.EntityFrameworkCore;

namespace barbeariaPro.Services;

public class AgendamentoService
{
    private readonly AppDbContext _context;

    public AgendamentoService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Agendamento>> ObterTodos()
    {
        return await _context.Agendamentos
            .Include(a => a.Servico)
            .Include(a => a.Cliente)
            .Include(a => a.Profissional)
            .ToListAsync();
    }

    public async Task<Agendamento?> ObterPorId(int id)
    {
        return await _context.Agendamentos
            .Include(a => a.Servico)
            .Include(a => a.Cliente)
            .Include(a => a.Profissional)
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<Agendamento> Adicionar(Agendamento agendamento)
    {
        var profissionalExistente = await _context.Profissionais
            .FirstOrDefaultAsync(p => p.Id == agendamento.ProfissionalFk);
        if (profissionalExistente == null)
            throw new Exception("Profissional não encontrado.");

        var clienteExistente = await _context.Clientes
            .FirstOrDefaultAsync(c => c.Id == agendamento.ClienteFk);
        if (clienteExistente == null)
            throw new Exception("Cliente não encontrado.");

        var servicoExistente = await _context.Servicos
            .FirstOrDefaultAsync(s => s.Id == agendamento.ServicoFk);
        if (servicoExistente == null)
            throw new Exception("Serviço não encontrado.");

        _context.Agendamentos.Add(agendamento);
        await _context.SaveChangesAsync();
        return agendamento;
    }

    public async Task Atualizar(Agendamento agendamento)
    {
        var agendamentoExistente = await _context.Agendamentos
            .FirstOrDefaultAsync(a => a.Id == agendamento.Id);
        if (agendamentoExistente == null)
            throw new Exception("Agendamento não encontrado.");

        _context.Agendamentos.Update(agendamento);
        await _context.SaveChangesAsync();
    }

    public async Task Deletar(Agendamento agendamento)
    {
        var agendamentoExistente = await _context.Agendamentos
            .FirstOrDefaultAsync(a => a.Id == agendamento.Id);
        if (agendamentoExistente == null)
            throw new Exception("Agendamento não encontrado.");

        _context.Agendamentos.Remove(agendamento);
        await _context.SaveChangesAsync();
    }
}
