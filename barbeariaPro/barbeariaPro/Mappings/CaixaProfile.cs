using AutoMapper;
using barbeariaPro.Models;
using barbeariaPro.DTOs;

namespace barbeariaPro.Mappings;

public class CaixaProfile : Profile
{
    public CaixaProfile()
    {
        CreateMap<Caixa, CaixaDTO>()
            .ForMember(dest => dest.NomeUsuario, opt => opt.MapFrom(src => src.Usuario.Email));

        CreateMap<CaixaDTO, Caixa>();
    }
}
