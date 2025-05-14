using barbeariaPro.Services;
using barbeariaPro.DTOs;
using barbeariaPro.Models;
using Microsoft.AspNetCore.Mvc;

namespace barbeariaPro.Controllers;

[ApiController]
[Route("clientes")]
public class ClienteController : ControllerBase
{
    private readonly ClienteService _clienteService;

    public ClienteController(ClienteService clienteService)
    {
        _clienteService = clienteService;
    }

    [HttpGet]
    public async Task<IActionResult> GetClientes()
    {
        var clientes = await _clienteService.ObterTodosClientes();
        return Ok(clientes);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetClientePorId(int id)
    {
        var cliente = await _clienteService.ObterClientePorId(id);
        
        if (cliente == null)
        {
            return NotFound("Cliente não encontrado");
        }
        
        return Ok(cliente);
    }

    [HttpPost("cadastrar")]
    public async Task<IActionResult> AdicionarCliente(ClienteDTO clienteDto)
    {
        var cliente = new Cliente
        {
            Nome = clienteDto.Nome,
            Sobrenome = clienteDto.Sobrenome,
            Telefone = clienteDto.Telefone,
            Email = clienteDto.Email,
            Cpf = clienteDto.Cpf,
            DataNascimento = clienteDto.DataNascimento
        };

        var novoCliente = await _clienteService.AdicionarCliente(cliente);
        return CreatedAtAction(nameof(GetClientePorId), new { id = novoCliente.Id }, novoCliente);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> AtualizarCliente(int id, ClienteDTO clienteDto)
    {
        var clienteExistente = await _clienteService.ObterClientePorId(id);
        if (clienteExistente == null)
        {
            return NotFound("Cliente não encontrado");
        }

        clienteExistente.Nome = clienteDto.Nome;
        clienteExistente.Sobrenome = clienteDto.Sobrenome;
        clienteExistente.Telefone = clienteDto.Telefone;
        clienteExistente.Email = clienteDto.Email;
        clienteExistente.Cpf = clienteDto.Cpf;
        clienteExistente.DataNascimento = clienteDto.DataNascimento;

        await _clienteService.AtualizarCliente(clienteExistente);

        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletarCliente(int id)
    {
        var cliente = await _clienteService.ObterClientePorId(id);
        if (cliente == null)
        {
            return NotFound("Cliente não encontrado");
        }

        await _clienteService.DeletarCliente(cliente);

        return NoContent();
    }

}