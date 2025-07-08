using AutoMapper;
using barbeariaPro.DTOs;
using barbeariaPro.Models;

namespace barbeariaPro.Mappings;

public class UsuarioProfile : Profile
{
    public UsuarioProfile()
    {
        CreateMap<Usuario, UsuarioDTO>().ReverseMap();
    }
}
