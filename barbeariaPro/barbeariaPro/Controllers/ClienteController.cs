using AutoMapper;
using barbeariaPro.DTOs;
using barbeariaPro.Models;
using barbeariaPro.Services;
using Microsoft.AspNetCore.Mvc;

namespace barbeariaPro.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClienteController : ControllerBase
{
    private readonly ClienteService _clienteService;
    private readonly IMapper _mapper;

    public ClienteController(ClienteService clienteService, IMapper mapper)
    {
        _clienteService = clienteService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetTodos()
    {
        var clientes = await _clienteService.ObterTodosClientes();
        return Ok(_mapper.Map<List<ClienteDTO>>(clientes));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPorId(int id)
    {
        var cliente = await _clienteService.ObterClientePorId(id);
        if (cliente == null) return NotFound("Cliente não encontrado.");
        return Ok(_mapper.Map<ClienteDTO>(cliente));
    }

    [HttpPost]
    public async Task<IActionResult> Adicionar([FromBody] ClienteDTO clienteDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        if (await _clienteService.CpfExiste(clienteDto.Cpf))
        {
            return Conflict("Já existe um cliente com esse CPF.");
        }

        var cliente = _mapper.Map<Cliente>(clienteDto);
        var novoCliente = await _clienteService.AdicionarCliente(cliente);
        return CreatedAtAction(nameof(GetPorId), new { id = novoCliente.Id }, _mapper.Map<ClienteDTO>(novoCliente));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Atualizar(int id, [FromBody] ClienteDTO clienteDto)
    {
        var clienteExistente = await _clienteService.ObterClientePorId(id);
        if (clienteExistente == null) return NotFound("Cliente não encontrado.");

        _mapper.Map(clienteDto, clienteExistente);
        await _clienteService.AtualizarCliente(clienteExistente);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Deletar(int id)
    {
        var clienteExistente = await _clienteService.ObterClientePorId(id);
        if (clienteExistente == null) return NotFound("Cliente não encontrado.");

        await _clienteService.DeletarCliente(clienteExistente);
        return NoContent();
    }
}
