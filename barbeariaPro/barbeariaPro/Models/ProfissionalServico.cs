namespace barbeariaPro.Models;

public class ProfissionalServico
{
    public int Id { get; set; }

    public int ServicoFk { get; set; }
    public Servico Servico { get; set; }

    public int ProfissionalFk { get; set; }
    public Profissional Profissional { get; set; }
}