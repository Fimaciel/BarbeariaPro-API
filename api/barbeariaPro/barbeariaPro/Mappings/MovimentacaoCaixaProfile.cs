using AutoMapper;
using barbeariaPro.Models;
using barbeariaPro.DTOs;

namespace barbeariaPro.Mappings;

public class MovimentacaoCaixaProfile : Profile
{
    public MovimentacaoCaixaProfile()
    {
        CreateMap<MovimentacaoCaixa, MovimentacaoCaixaDTO>();

        CreateMap<MovimentacaoCaixaDTO, MovimentacaoCaixa>();
    }
}
