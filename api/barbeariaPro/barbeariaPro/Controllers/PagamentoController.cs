using AutoMapper;
using barbeariaPro.DTOs;
using barbeariaPro.Models;
using barbeariaPro.Services;
using Microsoft.AspNetCore.Mvc;

namespace barbeariaPro.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PagamentoController : ControllerBase
{
    private readonly PagamentoService _pagamentoService;
    private readonly IMapper _mapper;

    public PagamentoController(PagamentoService pagamentoService, IMapper mapper)
    {
        _pagamentoService = pagamentoService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetTodos()
    {
        var pagamentos = await _pagamentoService.ObterTodos();
        return Ok(_mapper.Map<List<PagamentoDTO>>(pagamentos));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPorId(int id)
    {
        var pagamento = await _pagamentoService.ObterPorId(id);
        if (pagamento == null) return NotFound("Pagamento não encontrado.");
        return Ok(_mapper.Map<PagamentoDTO>(pagamento));
    }

    [HttpPost]
    public async Task<IActionResult> Adicionar([FromBody] PagamentoDTO pagamentoDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var pagamento = _mapper.Map<Pagamento>(pagamentoDto);
        var novoPagamento = await _pagamentoService.Adicionar(pagamento);
        return CreatedAtAction(nameof(GetPorId), new { id = novoPagamento.Id }, _mapper.Map<PagamentoDTO>(novoPagamento));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Atualizar(int id, [FromBody] PagamentoDTO pagamentoDto)
    {
        var pagamentoExistente = await _pagamentoService.ObterPorId(id);
        if (pagamentoExistente == null) return NotFound("Pagamento não encontrado.");

        _mapper.Map(pagamentoDto, pagamentoExistente);
        await _pagamentoService.Atualizar(pagamentoExistente);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Deletar(int id)
    {
        var pagamentoExistente = await _pagamentoService.ObterPorId(id);
        if (pagamentoExistente == null) return NotFound("Pagamento não encontrado.");

        await _pagamentoService.Deletar(pagamentoExistente);
        return NoContent();
    }
}
