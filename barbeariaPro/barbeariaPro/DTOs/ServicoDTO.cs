﻿namespace barbeariaPro.DTOs;

public class ServicoDTO
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public decimal Valor { get; set; }
    public DateTime DuracaoEstimada { get; set; }
    public int DuracaoMinutos { get; set; }
    public string Categoria { get; set; }
}
