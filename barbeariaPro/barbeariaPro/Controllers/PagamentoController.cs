using AutoMapper;
using barbeariaPro.DTOs;
using barbeariaPro.Models;
using barbeariaPro.Services;
using Microsoft.AspNetCore.Mvc;

namespace barbeariaPro.Controllers;

[ApiController]
[Route("pagamentos")]
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
        var lista = await _pagamentoService.ObterTodos();
        return Ok(_mapper.Map<List<PagamentoDTO>>(lista));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPorId(int id)
    {
        var pagamento = await _pagamentoService.ObterPorId(id);
        if (pagamento == null) return NotFound("Pagamento não encontrado.");

        return Ok(_mapper.Map<PagamentoDTO>(pagamento));
    }

    [HttpPost]
    public async Task<IActionResult> Adicionar([FromBody] PagamentoDTO dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var pagamento = _mapper.Map<Pagamento>(dto);
        var novo = await _pagamentoService.Adicionar(pagamento);

        return CreatedAtAction(nameof(GetPorId), new { id = novo.Id }, _mapper.Map<PagamentoDTO>(novo));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Atualizar(int id, [FromBody] PagamentoDTO dto)
    {
        var existente = await _pagamentoService.ObterPorId(id);
        if (existente == null) return NotFound("Pagamento não encontrado.");

        _mapper.Map(dto, existente);
        await _pagamentoService.Atualizar(existente);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Deletar(int id)
    {
        var existente = await _pagamentoService.ObterPorId(id);
        if (existente == null) return NotFound("Pagamento não encontrado.");

        await _pagamentoService.Deletar(existente);
        return NoContent();
    }
}
