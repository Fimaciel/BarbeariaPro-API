using AutoMapper;
using barbeariaPro.DTOs;
using barbeariaPro.Models;
using barbeariaPro.Services;
using Microsoft.AspNetCore.Mvc;

namespace barbeariaPro.Controllers;

[ApiController]
[Route("api/[controller]")]
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
        var usuarios = await _usuarioService.ObterTodos();
        return Ok(_mapper.Map<List<UsuarioDTO>>(usuarios));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPorId(int id)
    {
        var usuario = await _usuarioService.ObterPorId(id);
        if (usuario == null) return NotFound("Usuário não encontrado.");
        return Ok(_mapper.Map<UsuarioDTO>(usuario));
    }

    [HttpPost]
    public async Task<IActionResult> Adicionar([FromBody] UsuarioDTO usuarioDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var usuario = _mapper.Map<Usuario>(usuarioDto);
        var novoUsuario = await _usuarioService.Adicionar(usuario);
        return CreatedAtAction(nameof(GetPorId), new { id = novoUsuario.Id }, _mapper.Map<UsuarioDTO>(novoUsuario));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Atualizar(int id, [FromBody] UsuarioDTO usuarioDto)
    {
        var usuarioExistente = await _usuarioService.ObterPorId(id);
        if (usuarioExistente == null) return NotFound("Usuário não encontrado.");

        _mapper.Map(usuarioDto, usuarioExistente);
        await _usuarioService.Atualizar(usuarioExistente);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Deletar(int id)
    {
        var usuarioExistente = await _usuarioService.ObterPorId(id);
        if (usuarioExistente == null) return NotFound("Usuário não encontrado.");

        await _usuarioService.Deletar(usuarioExistente);
        return NoContent();
    }
}
