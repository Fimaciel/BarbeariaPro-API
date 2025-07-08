using AutoMapper;
using barbeariaPro.DTOs;
using barbeariaPro.Models;
using barbeariaPro.Services;
using Microsoft.AspNetCore.Mvc;

namespace barbeariaPro.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProfissionalServicoController : ControllerBase
{
    private readonly ProfissionalServicoService _profissionalServicoService;
    private readonly IMapper _mapper;

    public ProfissionalServicoController(ProfissionalServicoService profissionalServicoService, IMapper mapper)
    {
        _profissionalServicoService = profissionalServicoService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetTodos()
    {
        var profissionalServicos = await _profissionalServicoService.ObterTodos();
        return Ok(_mapper.Map<List<ProfissionalServicoDTO>>(profissionalServicos));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPorId(int id)
    {
        var profissionalServico = await _profissionalServicoService.ObterPorId(id);
        if (profissionalServico == null) return NotFound("Profissional-Serviço não encontrado.");
        return Ok(_mapper.Map<ProfissionalServicoDTO>(profissionalServico));
    }

    [HttpPost]
    public async Task<IActionResult> Adicionar([FromBody] ProfissionalServicoDTO profissionalServicoDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var profissionalServico = _mapper.Map<ProfissionalServico>(profissionalServicoDto);
        var novoProfissionalServico = await _profissionalServicoService.Adicionar(profissionalServico);
        return CreatedAtAction(nameof(GetPorId), new { id = novoProfissionalServico.Id }, _mapper.Map<ProfissionalServicoDTO>(novoProfissionalServico));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Atualizar(int id, [FromBody] ProfissionalServicoDTO profissionalServicoDto)
    {
        var profissionalServicoExistente = await _profissionalServicoService.ObterPorId(id);
        if (profissionalServicoExistente == null) return NotFound("Profissional-Serviço não encontrado.");

        _mapper.Map(profissionalServicoDto, profissionalServicoExistente);
        await _profissionalServicoService.Atualizar(profissionalServicoExistente);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Deletar(int id)
    {
        var profissionalServicoExistente = await _profissionalServicoService.ObterPorId(id);
        if (profissionalServicoExistente == null) return NotFound("Profissional-Serviço não encontrado.");

        await _profissionalServicoService.Deletar(profissionalServicoExistente);
        return NoContent();
    }
}
