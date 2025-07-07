using AutoMapper;
using barbeariaPro.DTOs;
using barbeariaPro.Models;

namespace barbeariaPro.Mappings;

public class ProfissionalProfile : Profile
{
    public ProfissionalProfile()
    {
        CreateMap<Profissional, ProfissionalDTO>().ReverseMap();
    }
}
