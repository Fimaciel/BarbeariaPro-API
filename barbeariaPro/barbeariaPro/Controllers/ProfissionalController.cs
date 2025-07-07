using AutoMapper;
using barbeariaPro.DTOs;
using barbeariaPro.Models;
using barbeariaPro.Services;
using Microsoft.AspNetCore.Mvc;

namespace barbeariaPro.Controllers;

[ApiController]
[Route("profissionais")]
public class ProfissionalController : ControllerBase
{
    private readonly ProfissionalService _profissionalService;
    private readonly IMapper _mapper;

    public ProfissionalController(ProfissionalService profissionalService, IMapper mapper)
    {
        _profissionalService = profissionalService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetTodos()
    {
        var lista = await _profissionalService.ObterTodos();
        return Ok(_mapper.Map<List<ProfissionalDTO>>(lista));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPorId(int id)
    {
        var profissional = await _profissionalService.ObterPorId(id);
        if (profissional == null) return NotFound("Profissional não encontrado.");

        return Ok(_mapper.Map<ProfissionalDTO>(profissional));
    }

    [HttpPost]
    public async Task<IActionResult> Adicionar([FromBody] ProfissionalDTO dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        if (await _profissionalService.CpfExiste(dto.CPF))
            return Conflict("Já existe um profissional com esse CPF.");

        var profissional = _mapper.Map<Profissional>(dto);
        var novo = await _profissionalService.Adicionar(profissional);

        return CreatedAtAction(nameof(GetPorId), new { id = novo.Id }, _mapper.Map<ProfissionalDTO>(novo));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Atualizar(int id, [FromBody] ProfissionalDTO dto)
    {
        var existente = await _profissionalService.ObterPorId(id);
        if (existente == null) return NotFound("Profissional não encontrado.");

        if (existente.CPF != dto.CPF && await _profissionalService.CpfExiste(dto.CPF))
            return Conflict("Já existe outro profissional com esse CPF.");

        _mapper.Map(dto, existente);
        await _profissionalService.Atualizar(existente);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Deletar(int id)
    {
        var existente = await _profissionalService.ObterPorId(id);
        if (existente == null) return NotFound("Profissional não encontrado.");

        await _profissionalService.Deletar(existente);
        return NoContent();
    }
}
