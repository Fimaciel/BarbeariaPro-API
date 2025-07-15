using AutoMapper;
using barbeariaPro.Models;
using barbeariaPro.DTOs;

namespace barbeariaPro.Mappings;

public class UsuarioProfile : Profile
{
    public UsuarioProfile()
    {
        CreateMap<Usuario, UsuarioDTO>()
            .ForMember(dest => dest.ProfissionalFk, opt => opt.MapFrom(src => src.ProfissionalFk));

        CreateMap<UsuarioDTO, Usuario>();
    }
}
