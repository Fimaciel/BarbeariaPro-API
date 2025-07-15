public class AgendamentoDTO
{
    public int Id { get; set; }
    public DateTime DataHorario { get; set; }
    public string Status { get; set; }
    public string Observacoes { get; set; }
    public string ?MotivoCancelamento { get; set; }

    public int ServicoFk { get; set; }
    public int ClienteFk { get; set; }
    public int ProfissionalFk { get; set; }
}
