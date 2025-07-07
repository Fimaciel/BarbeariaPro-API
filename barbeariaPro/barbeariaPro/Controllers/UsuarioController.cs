using AutoMapper;
using barbeariaPro.DTOs;
using barbeariaPro.Models;
using barbeariaPro.Services;
using Microsoft.AspNetCore.Mvc;

namespace barbeariaPro.Controllers;

[ApiController]
[Route("usuarios")]
public class UsuarioController : ControllerBase
{
    private readonly UsuarioService _usuarioService;
    private readonly IMapper _mapper;

    public UsuarioController(UsuarioService usuarioService, IMapper mapper)
    {
        _usuarioService = usuarioService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetTodos()
    {
        var lista = await _usuarioService.ObterTodos();
        return Ok(_mapper.Map<List<UsuarioDTO>>(lista));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPorId(int id)
    {
        var usuario = await _usuarioService.ObterPorId(id);
        if (usuario == null) return NotFound("Usuário não encontrado.");
        return Ok(_mapper.Map<UsuarioDTO>(usuario));
    }

    [HttpPost]
    public async Task<IActionResult> Adicionar([FromBody] UsuarioDTO dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var usuario = _mapper.Map<Usuario>(dto);
        var novo = await _usuarioService.Adicionar(usuario);
        return CreatedAtAction(nameof(GetPorId), new { id = novo.Id }, _mapper.Map<UsuarioDTO>(novo));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Atualizar(int id, [FromBody] UsuarioDTO dto)
    {
        var existente = await _usuarioService.ObterPorId(id);
        if (existente == null) return NotFound("Usuário não encontrado.");

        _mapper.Map(dto, existente);
        await _usuarioService.Atualizar(existente);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Deletar(int id)
    {
        var existente = await _usuarioService.ObterPorId(id);
        if (existente == null) return NotFound("Usuário não encontrado.");

        await _usuarioService.Deletar(existente);
        return NoContent();
    }
}
