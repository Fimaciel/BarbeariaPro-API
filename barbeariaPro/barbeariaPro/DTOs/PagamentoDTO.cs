﻿namespace barbeariaPro.DTOs;

public class PagamentoDTO
{
    public int Id { get; set; }
    public string FormaPagamento { get; set; }
    public bool Status { get; set; }
    public decimal Valor { get; set; }
    public string ComprovantePath { get; set; }
    public DateTime DataEstorno { get; set; }

    public int AgendamentoFk { get; set; }
}
