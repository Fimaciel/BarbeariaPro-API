using AutoMapper;
using barbeariaPro.Models;
using barbeariaPro.DTOs;

namespace barbeariaPro.Mappings;

public class ProfissionalProfile : Profile
{
    public ProfissionalProfile()
    {
        CreateMap<Profissional, ProfissionalDTO>();
        CreateMap<ProfissionalDTO, Profissional>();
    }
}
