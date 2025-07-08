using AutoMapper;
using barbeariaPro.Models;
using barbeariaPro.DTOs;

namespace barbeariaPro.Mappings;

public class ServicoProfile : Profile
{
    public ServicoProfile()
    {
        CreateMap<Servico, ServicoDTO>();

        CreateMap<ServicoDTO, Servico>();
    }
}
