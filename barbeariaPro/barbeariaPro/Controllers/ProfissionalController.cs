using AutoMapper;
using barbeariaPro.DTOs;
using barbeariaPro.Models;
using barbeariaPro.Services;
using Microsoft.AspNetCore.Mvc;

namespace barbeariaPro.Controllers;

[ApiController]
[Route("api/[controller]")]
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
        var profissionais = await _profissionalService.ObterTodos();
        return Ok(_mapper.Map<List<ProfissionalDTO>>(profissionais));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPorId(int id)
    {
        var profissional = await _profissionalService.ObterPorId(id);
        if (profissional == null) return NotFound("Profissional não encontrado.");
        return Ok(_mapper.Map<ProfissionalDTO>(profissional));
    }

    [HttpPost]
    public async Task<IActionResult> Adicionar([FromBody] ProfissionalDTO profissionalDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var profissional = _mapper.Map<Profissional>(profissionalDto);
        var novoProfissional = await _profissionalService.Adicionar(profissional);
        return CreatedAtAction(nameof(GetPorId), new { id = novoProfissional.Id }, _mapper.Map<ProfissionalDTO>(novoProfissional));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Atualizar(int id, [FromBody] ProfissionalDTO profissionalDto)
    {
        var profissionalExistente = await _profissionalService.ObterPorId(id);
        if (profissionalExistente == null) return NotFound("Profissional não encontrado.");

        _mapper.Map(profissionalDto, profissionalExistente);
        await _profissionalService.Atualizar(profissionalExistente);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Deletar(int id)
    {
        var profissionalExistente = await _profissionalService.ObterPorId(id);
        if (profissionalExistente == null) return NotFound("Profissional não encontrado.");

        await _profissionalService.Deletar(profissionalExistente);
        return NoContent();
    }
}
