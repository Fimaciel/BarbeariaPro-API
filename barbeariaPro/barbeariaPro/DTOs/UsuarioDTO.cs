namespace barbeariaPro.DTOs;

public class UsuarioDTO
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Perfil { get; set; }

    public int ProfissionalFk { get; set; }
}