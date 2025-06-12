namespace barbeariaPro.Models;

public class Usuario
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Senha { get; set; }
    public string Perfil { get; set; }

    public int ProfissionalFk { get; set; }
    public Profissional Profissional { get; set; }
}
