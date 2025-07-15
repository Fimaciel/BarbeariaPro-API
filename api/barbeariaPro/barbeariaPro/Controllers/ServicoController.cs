using AutoMapper;
using barbeariaPro.DTOs;
using barbeariaPro.Models;
using barbeariaPro.Services;
using Microsoft.AspNetCore.Mvc;

namespace barbeariaPro.Controllers;

[ApiController]
[Route("api/[controller]")]
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
        var servicos = await _servicoService.ObterTodos();
        return Ok(_mapper.Map<List<ServicoDTO>>(servicos));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPorId(int id)
    {
        var servico = await _servicoService.ObterPorId(id);
        if (servico == null) return NotFound("Serviço não encontrado.");
        return Ok(_mapper.Map<ServicoDTO>(servico));
    }

    [HttpPost]
    public async Task<IActionResult> Adicionar([FromBody] ServicoDTO servicoDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var servico = _mapper.Map<Servico>(servicoDto);
        var novoServico = await _servicoService.Adicionar(servico);
        return CreatedAtAction(nameof(GetPorId), new { id = novoServico.Id }, _mapper.Map<ServicoDTO>(novoServico));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Atualizar(int id, [FromBody] ServicoDTO servicoDto)
    {
        var servicoExistente = await _servicoService.ObterPorId(id);
        if (servicoExistente == null) return NotFound("Serviço não encontrado.");

        _mapper.Map(servicoDto, servicoExistente);
        await _servicoService.Atualizar(servicoExistente);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Deletar(int id)
    {
        var servicoExistente = await _servicoService.ObterPorId(id);
        if (servicoExistente == null) return NotFound("Serviço não encontrado.");

        await _servicoService.Deletar(servicoExistente);
        return NoContent();
    }
}
