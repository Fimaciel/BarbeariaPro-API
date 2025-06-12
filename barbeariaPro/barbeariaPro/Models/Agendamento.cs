using barbeariaPro.Models;

public class Agendamento
{
    public int Id { get; set; }
    public DateTime DataHorario { get; set; }
    public string Status { get; set; }
    public string Observacoes { get; set; }
    public string MotivoCancelamento { get; set; }

    public int ServicoFk { get; set; }
    public Servico Servico { get; set; }

    public int ClienteFk { get; set; }
    public Cliente Cliente { get; set; }

    public int ProfissionalFk { get; set; }
    public Profissional Profissional { get; set; }

    public ICollection<Pagamento> Pagamentos { get; set; }
}