using AutoMapper;
using barbeariaPro.DTOs;
using barbeariaPro.Models;
using barbeariaPro.Services;
using Microsoft.AspNetCore.Mvc;

namespace barbeariaPro.Controllers;

[ApiController]
[Route("caixas")]
public class CaixaController : ControllerBase
{
    private readonly CaixaService _caixaService;
    private readonly IMapper _mapper;

    public CaixaController(CaixaService caixaService, IMapper mapper)
    {
        _caixaService = caixaService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetTodos()
    {
        var caixas = await _caixaService.ObterTodos();
        return Ok(_mapper.Map<List<CaixaDTO>>(caixas));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPorId(int id)
    {
        var caixa = await _caixaService.ObterPorId(id);
        if (caixa == null) return NotFound("Caixa não encontrado.");

        return Ok(_mapper.Map<CaixaDTO>(caixa));
    }

    [HttpPost]
    public async Task<IActionResult> Adicionar([FromBody] CaixaDTO dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var caixa = _mapper.Map<Caixa>(dto);
        var novoCaixa = await _caixaService.Adicionar(caixa);

        return CreatedAtAction(nameof(GetPorId), new { id = novoCaixa.Id }, _mapper.Map<CaixaDTO>(novoCaixa));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Atualizar(int id, [FromBody] CaixaDTO dto)
    {
        var caixaExistente = await _caixaService.ObterPorId(id);
        if (caixaExistente == null) return NotFound("Caixa não encontrado.");

        _mapper.Map(dto, caixaExistente);
        await _caixaService.Atualizar(caixaExistente);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Deletar(int id)
    {
        var caixa = await _caixaService.ObterPorId(id);
        if (caixa == null) return NotFound("Caixa não encontrado.");

        await _caixaService.Deletar(caixa);
        return NoContent();
    }
}