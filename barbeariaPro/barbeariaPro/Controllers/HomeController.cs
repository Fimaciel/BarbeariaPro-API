using AutoMapper;
using barbeariaPro.DTOs;
using barbeariaPro.Models;
using barbeariaPro.Services;
using Microsoft.AspNetCore.Mvc;

namespace barbeariaPro.Controllers;

[ApiController]
[Route("movimentacoes")]
public class MovimentacaoCaixaController : ControllerBase
{
    private readonly MovimentacaoCaixaService _movimentacaoService;
    private readonly IMapper _mapper;

    public MovimentacaoCaixaController(MovimentacaoCaixaService movimentacaoService, IMapper mapper)
    {
        _movimentacaoService = movimentacaoService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetTodas()
    {
        var lista = await _movimentacaoService.ObterTodas();
        return Ok(_mapper.Map<List<MovimentacaoCaixaDTO>>(lista));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPorId(int id)
    {
        var mov = await _movimentacaoService.ObterPorId(id);
        if (mov == null) return NotFound("Movimentação não encontrada.");

        return Ok(_mapper.Map<MovimentacaoCaixaDTO>(mov));
    }

    [HttpPost]
    public async Task<IActionResult> Adicionar([FromBody] MovimentacaoCaixaDTO dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var movimentacao = _mapper.Map<MovimentacaoCaixa>(dto);
        var nova = await _movimentacaoService.Adicionar(movimentacao);

        return CreatedAtAction(nameof(GetPorId), new { id = nova.Id }, _mapper.Map<MovimentacaoCaixaDTO>(nova));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Atualizar(int id, [FromBody] MovimentacaoCaixaDTO dto)
    {
        var existente = await _movimentacaoService.ObterPorId(id);
        if (existente == null) return NotFound("Movimentação não encontrada.");

        _mapper.Map(dto, existente);
        await _movimentacaoService.Atualizar(existente);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Deletar(int id)
    {
        var existente = await _movimentacaoService.ObterPorId(id);
        if (existente == null) return NotFound("Movimentação não encontrada.");

        await _movimentacaoService.Deletar(existente);
        return NoContent();
    }
}
