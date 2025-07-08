using AutoMapper;
using barbeariaPro.DTOs;
using barbeariaPro.Models;
using barbeariaPro.Services;
using Microsoft.AspNetCore.Mvc;

namespace barbeariaPro.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MovimentacaoCaixaController : ControllerBase
{
    private readonly MovimentacaoCaixaService _movimentacaoCaixaService;
    private readonly IMapper _mapper;

    public MovimentacaoCaixaController(MovimentacaoCaixaService movimentacaoCaixaService, IMapper mapper)
    {
        _movimentacaoCaixaService = movimentacaoCaixaService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetTodos()
    {
        var movimentacoes = await _movimentacaoCaixaService.ObterTodos();
        return Ok(_mapper.Map<List<MovimentacaoCaixaDTO>>(movimentacoes));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPorId(int id)
    {
        var movimentacao = await _movimentacaoCaixaService.ObterPorId(id);
        if (movimentacao == null) return NotFound("Movimentação de caixa não encontrada.");
        return Ok(_mapper.Map<MovimentacaoCaixaDTO>(movimentacao));
    }

    [HttpPost]
    public async Task<IActionResult> Adicionar([FromBody] MovimentacaoCaixaDTO movimentacaoDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var movimentacao = _mapper.Map<MovimentacaoCaixa>(movimentacaoDto);
        var novaMovimentacao = await _movimentacaoCaixaService.Adicionar(movimentacao);
        return CreatedAtAction(nameof(GetPorId), new { id = novaMovimentacao.Id }, _mapper.Map<MovimentacaoCaixaDTO>(novaMovimentacao));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Atualizar(int id, [FromBody] MovimentacaoCaixaDTO movimentacaoDto)
    {
        var movimentacaoExistente = await _movimentacaoCaixaService.ObterPorId(id);
        if (movimentacaoExistente == null) return NotFound("Movimentação de caixa não encontrada.");

        _mapper.Map(movimentacaoDto, movimentacaoExistente);
        await _movimentacaoCaixaService.Atualizar(movimentacaoExistente);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Deletar(int id)
    {
        var movimentacaoExistente = await _movimentacaoCaixaService.ObterPorId(id);
        if (movimentacaoExistente == null) return NotFound("Movimentação de caixa não encontrada.");

        await _movimentacaoCaixaService.Deletar(movimentacaoExistente);
        return NoContent();
    }
}
