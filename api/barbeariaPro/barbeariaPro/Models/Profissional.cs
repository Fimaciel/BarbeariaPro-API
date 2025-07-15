namespace barbeariaPro.Models;
public class Profissional
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Sobrenome { get; set; }
    public string Telefone { get; set; }
    public string CPF { get; set; }
    public string Email { get; set; }
    public DateTime DataNasc { get; set; }
    public string Especialidade { get; set; }
    public DateTime? DataExclusao { get; set; }

    public ICollection<Agendamento> Agendamentos { get; set; }
    public ICollection<ProfissionalServico> Servicos { get; set; }

    public Usuario Usuario { get; set; }
}
