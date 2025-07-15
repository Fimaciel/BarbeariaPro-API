using AutoMapper;
using barbeariaPro.Models;
using barbeariaPro.DTOs;

namespace barbeariaPro.Mappings;

public class ProfissionalServicoProfile : Profile
{
    public ProfissionalServicoProfile()
    {
        CreateMap<ProfissionalServico, ProfissionalServicoDTO>();

        CreateMap<ProfissionalServicoDTO, ProfissionalServico>();
    }
}
