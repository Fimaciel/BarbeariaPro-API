using AutoMapper;
using barbeariaPro.DTOs;
using barbeariaPro.Models;
using barbeariaPro.Services;
using Microsoft.AspNetCore.Mvc;

namespace barbeariaPro.Controllers;

[ApiController]
[Route("clientes")]
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
    public async Task<IActionResult> GetClientes()
    {
        var clientes = await _clienteService.ObterTodosClientes();
        var clienteDtos = _mapper.Map<List<ClienteDTO>>(clientes);
        return Ok(clienteDtos);
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
    public async Task<IActionResult> AdicionarCliente([FromBody] ClienteDTO clienteDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (await _clienteService.CpfExiste(clienteDto.Cpf))
            return Conflict("Já existe um cliente com esse CPF.");

        var cliente = _mapper.Map<Cliente>(clienteDto);
        var novoCliente = await _clienteService.AdicionarCliente(cliente);

        var resultDto = _mapper.Map<ClienteDTO>(novoCliente);
        return CreatedAtAction(nameof(GetClientePorId), new { id = novoCliente.Id }, resultDto);
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> AtualizarCliente(int id, [FromBody] ClienteDTO clienteDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var clienteExistente = await _clienteService.ObterClientePorId(id);
        if (clienteExistente == null) return NotFound("Cliente não encontrado");

        if (clienteExistente.Cpf != clienteDto.Cpf && await _clienteService.CpfExiste(clienteDto.Cpf))
            return Conflict("Já existe outro cliente com esse CPF.");

        _mapper.Map(clienteDto, clienteExistente);
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
