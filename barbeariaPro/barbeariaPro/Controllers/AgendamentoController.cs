using barbeariaPro.dbContext;
using barbeariaPro.Services;
using barbeariaPro.DTOs;
using barbeariaPro.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class AgendamentosController : ControllerBase
{
    private readonly AppDbContext _context;

    public AgendamentosController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AgendamentoDto>>> GetAgendamentos()
    {
        var agendamentos = await _context.Agendamentos
            .Select(a => new AgendamentoDto
            {
                Id = a.Id,
                DataHorario = a.DataHorario,
                Status = a.Status,
                Observacoes = a.Observacoes,
                MotivoCancelamento = a.MotivoCancelamento,
                ServicoFk = a.ServicoFk,
                ClienteFk = a.ClienteFk,
                ProfissionalFk = a.ProfissionalFk
            }).ToListAsync();

        return Ok(agendamentos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AgendamentoDto>> GetAgendamento(int id)
    {
        var a = await _context.Agendamentos.FindAsync(id);
        if (a == null) return NotFound();

        return new AgendamentoDto
        {
            Id = a.Id,
            DataHorario = a.DataHorario,
            Status = a.Status,
            Observacoes = a.Observacoes,
            MotivoCancelamento = a.MotivoCancelamento,
            ServicoFk = a.ServicoFk,
            ClienteFk = a.ClienteFk,
            ProfissionalFk = a.ProfissionalFk
        };
    }

    [HttpPost]
    public async Task<ActionResult> PostAgendamento(AgendamentoDto dto)
    {
        var agendamento = new Agendamento
        {
            DataHorario = dto.DataHorario,
            Status = dto.Status,
            Observacoes = dto.Observacoes,
            MotivoCancelamento = dto.MotivoCancelamento,
            ServicoFk = dto.ServicoFk,
            ClienteFk = dto.ClienteFk,
            ProfissionalFk = dto.ProfissionalFk
        };

        _context.Agendamentos.Add(agendamento);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetAgendamento), new { id = agendamento.Id }, agendamento);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAgendamento(int id, AgendamentoDto dto)
    {
        var agendamento = await _context.Agendamentos.FindAsync(id);
        if (agendamento == null) return NotFound();

        agendamento.DataHorario = dto.DataHorario;
        agendamento.Status = dto.Status;
        agendamento.Observacoes = dto.Observacoes;
        agendamento.MotivoCancelamento = dto.MotivoCancelamento;
        agendamento.ServicoFk = dto.ServicoFk;
        agendamento.ClienteFk = dto.ClienteFk;
        agendamento.ProfissionalFk = dto.ProfissionalFk;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAgendamento(int id)
    {
        var agendamento = await _context.Agendamentos.FindAsync(id);
        if (agendamento == null) return NotFound();

        _context.Agendamentos.Remove(agendamento);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
