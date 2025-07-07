using AutoMapper;
using barbeariaPro.DTOs;
using barbeariaPro.Models;
using barbeariaPro.Services;
using Microsoft.AspNetCore.Mvc;

namespace barbeariaPro.Controllers;

[ApiController]
[Route("servicos")]
public class ServicoController : ControllerBase
{
    private readonly ServicoService _servicoService;
    private readonly IMapper _mapper;

    public ServicoController(ServicoService servicoService, IMapper mapper)
    {
        _servicoService = servicoService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetTodos()
    {
        var lista = await _servicoService.ObterTodos();
        return Ok(_mapper.Map<List<ServicoDTO>>(lista));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPorId(int id)
    {
        var servico = await _servicoService.ObterPorId(id);
        if (servico == null) return NotFound("Serviço não encontrado.");
        return Ok(_mapper.Map<ServicoDTO>(servico));
    }

    [HttpPost]
    public async Task<IActionResult> Adicionar([FromBody] ServicoDTO dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var servico = _mapper.Map<Servico>(dto);
        var novo = await _servicoService.Adicionar(servico);
        return CreatedAtAction(nameof(GetPorId), new { id = novo.Id }, _mapper.Map<ServicoDTO>(novo));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Atualizar(int id, [FromBody] ServicoDTO dto)
    {
        var existente = await _servicoService.ObterPorId(id);
        if (existente == null) return NotFound("Serviço não encontrado.");

        _mapper.Map(dto, existente);
        await _servicoService.Atualizar(existente);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Deletar(int id)
    {
        var existente = await _servicoService.ObterPorId(id);
        if (existente == null) return NotFound("Serviço não encontrado.");

        await _servicoService.Deletar(existente);
        return NoContent();
    }
}
