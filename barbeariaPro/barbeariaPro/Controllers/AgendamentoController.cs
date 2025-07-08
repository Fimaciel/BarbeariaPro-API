using AutoMapper;
using barbeariaPro.DTOs;
using barbeariaPro.Models;
using barbeariaPro.Services;
using Microsoft.AspNetCore.Mvc;

namespace barbeariaPro.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AgendamentoController : ControllerBase
{
    private readonly AgendamentoService _agendamentoService;
    private readonly IMapper _mapper;

    public AgendamentoController(AgendamentoService agendamentoService, IMapper mapper)
    {
        _agendamentoService = agendamentoService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetTodos()
    {
        var agendamentos = await _agendamentoService.ObterTodos();
        return Ok(_mapper.Map<List<AgendamentoDTO>>(agendamentos));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPorId(int id)
    {
        var agendamento = await _agendamentoService.ObterPorId(id);
        if (agendamento == null) return NotFound("Agendamento não encontrado.");
        return Ok(_mapper.Map<AgendamentoDTO>(agendamento));
    }

    [HttpPost]
    public async Task<IActionResult> Adicionar([FromBody] AgendamentoDTO agendamentoDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var agendamento = _mapper.Map<Agendamento>(agendamentoDto);
        var novoAgendamento = await _agendamentoService.Adicionar(agendamento);
        return CreatedAtAction(nameof(GetPorId), new { id = novoAgendamento.Id }, _mapper.Map<AgendamentoDTO>(novoAgendamento));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Atualizar(int id, [FromBody] AgendamentoDTO agendamentoDto)
    {
        var agendamentoExistente = await _agendamentoService.ObterPorId(id);
        if (agendamentoExistente == null) return NotFound("Agendamento não encontrado.");

        _mapper.Map(agendamentoDto, agendamentoExistente);
        await _agendamentoService.Atualizar(agendamentoExistente);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Deletar(int id)
    {
        var agendamentoExistente = await _agendamentoService.ObterPorId(id);
        if (agendamentoExistente == null) return NotFound("Agendamento não encontrado.");

        await _agendamentoService.Deletar(agendamentoExistente);
        return NoContent();
    }
}
