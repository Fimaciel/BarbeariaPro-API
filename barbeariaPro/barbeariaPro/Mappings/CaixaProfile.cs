using AutoMapper;
using barbeariaPro.DTOs;
using barbeariaPro.Models;

public class CaixaProfile : Profile
{
    public CaixaProfile()
    {
        CreateMap<Caixa, CaixaDTO>()
            .ReverseMap();
    }
}
